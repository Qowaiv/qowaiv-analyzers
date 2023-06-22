using System.Reflection;

namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class DefinePropertiesAsNotNullable : CodingRule
{
    public DefinePropertiesAsNotNullable(): base(
        Rule.DefinePropertiesAsNotNullable,
        Rule.DefineEnumPropertiesAsNotNullable) { }

    protected override void Register(AnalysisContext context)
    {
        context.RegisterSyntaxNodeAction(ReportProperty, SyntaxKind.PropertyDeclaration);
        context.RegisterSyntaxNodeAction(ReportRecord, SyntaxKind.RecordDeclaration);
    }

    private void ReportProperty(SyntaxNodeAnalysisContext context)
        => Report(context.Node.Cast<PropertyDeclarationSyntax>().Type, context);

    private void ReportRecord(SyntaxNodeAnalysisContext context)
    {
        var types = context.Node.Cast<RecordDeclarationSyntax>()
            .ParameterList?.Parameters.Select(p => p.Type)
            .OfType<TypeSyntax>();

        foreach (var type in types ?? Array.Empty<TypeSyntax>())
        {
            Report(type, context);
        }
    }

    private static void Report(TypeSyntax syntax, SyntaxNodeAnalysisContext context)
    {
        if (context.SemanticModel.GetTypeInfo(syntax).Type is INamedTypeSymbol type
            && NotNullable(type) is { } diagnostic)
        {
            context.ReportDiagnostic(diagnostic, syntax);
        }
    }

    private static DiagnosticDescriptor? NotNullable(INamedTypeSymbol type)
        => type.IsValueType
        && type.IsNullableValueType()
        && type.TypeArguments[0] is INamedTypeSymbol inner
            ? EnumDefaultIsNoneOrEmpty(inner) ?? DefaultIsEmpty(inner)
            : null;

    private static DiagnosticDescriptor? EnumDefaultIsNoneOrEmpty(INamedTypeSymbol type)
        => type.TypeKind == TypeKind.Enum
        && type.GetMembers().Any(IsNoneOrEmpty)
        ? Rule.DefineEnumPropertiesAsNotNullable : null;

    private static DiagnosticDescriptor? DefaultIsEmpty(INamedTypeSymbol type)
        => type.GetMembers("Empty").Any(IsReadOnlyField)
        ? Rule.DefinePropertiesAsNotNullable : null;

    private static bool IsNoneOrEmpty(ISymbol member)
        => member is IFieldSymbol field
        && IsZero(field.ConstantValue)
        && (field.Name.ToUpperInvariant() == "NONE" || field.Name.ToUpperInvariant() == "EMPTY");

    private static bool IsZero(object? value) => Convert.ToInt64(value) == 0;

    private static bool IsReadOnlyField(ISymbol member)
        => member is IFieldSymbol field
        && field.IsReadOnly
        && field.IsStatic
        && field.Type.Equals(field.ContainingType, IncludeNullability: false)
        && field.IsPublic();
}
