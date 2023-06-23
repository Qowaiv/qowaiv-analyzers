using System.Reflection;

namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class DefinePropertiesAsNotNullable : CodingRule
{
    public DefinePropertiesAsNotNullable() : base(
        Rule.DefinePropertiesAsNotNullable,
        Rule.DefineEnumPropertiesAsNotNullable)
    { }

    protected override void Register(AnalysisContext context)
    {
        context.RegisterSyntaxNodeAction(ReportProperty, SyntaxKind.PropertyDeclaration);
        context.RegisterSyntaxNodeAction(ReportRecord, SyntaxKind.RecordDeclaration);
    }

    private void ReportProperty(SyntaxNodeAnalysisContext context)
        => Report(context.Node.Cast<PropertyDeclarationSyntax>().Type, context);

    private void ReportRecord(SyntaxNodeAnalysisContext context)
    {
        if (context.Node.Cast<RecordDeclarationSyntax>().ParameterList is { } pars)
        {
            foreach (var type in pars.Parameters.Select(p => p.Type))
            {
                Report(type, context);
            }
        }
    }

    private static void Report(TypeSyntax? syntax, SyntaxNodeAnalysisContext context)
    {
        foreach (var sub in Types(syntax))
        {
            if (context.SemanticModel.GetTypeInfo(sub).Type is INamedTypeSymbol type
                && NotNullable(type) is { } diagnostic)
            {
                context.ReportDiagnostic(diagnostic, sub);
            }
        }
    }

    private static IEnumerable<TypeSyntax> Types(TypeSyntax? type)
    {
        if (type is ArrayTypeSyntax array)
        {
            return Types(array.ElementType);
        }
        else if (type is GenericNameSyntax generic)
        {
            return type.Singleton().Concat(generic.TypeArgumentList.Arguments.SelectMany(Types));
        }
        else
        {
            return type.Singleton();
        }
    }

    private static DiagnosticDescriptor? NotNullable(INamedTypeSymbol type)
        => type.IsValueType
        && type.IsNullableValueType()
        && type.TypeArguments[0] is INamedTypeSymbol inner
            ? EnumDefaultIsNoneOrEmpty(inner) ?? DefaultIsEmpty(inner)
            : null;

    private static DiagnosticDescriptor? EnumDefaultIsNoneOrEmpty(INamedTypeSymbol type)
    {
        return type.TypeKind == TypeKind.Enum && type.GetMembers().Any(IsNoneOrEmpty)
            ? Rule.DefineEnumPropertiesAsNotNullable
            : null;

        static bool IsNoneOrEmpty(ISymbol member)
             => member is IFieldSymbol field
             && Convert.ToInt64(field.ConstantValue) == 0
             && (field.Name.ToUpperInvariant() == "NONE" || field.Name.ToUpperInvariant() == "EMPTY");
    }

    private static DiagnosticDescriptor? DefaultIsEmpty(INamedTypeSymbol type)
    {
        return type.GetMembers("Empty").Any(IsReadOnlyField)
            ? Rule.DefinePropertiesAsNotNullable
            : null;

        static bool IsReadOnlyField(ISymbol member)
           => member is IFieldSymbol field
           && field.IsReadOnly
           && field.IsStatic
           && field.Type.Equals(field.ContainingType, IncludeNullability: false)
           && field.IsPublic();
    }
}
