namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class DefinePropertiesAsNotNullable() : CodingRule(
    Rule.DefinePropertiesAsNotNullable,
    Rule.DefineEnumPropertiesAsNotNullable)
{
    protected override void Register(AnalysisContext context)
    {
        context.RegisterSyntaxNodeAction(ReportProperty, SyntaxKind.PropertyDeclaration);
        context.RegisterSyntaxNodeAction(ReportRecord, SyntaxKind.RecordDeclaration);
    }

    private void ReportProperty(SyntaxNodeAnalysisContext context)
        => Report(context.Node.Cast<PropertyDeclarationSyntax>().Type, context);

    private void ReportRecord(SyntaxNodeAnalysisContext context)
    {
        foreach (var type in context.Node.Cast<RecordDeclarationSyntax>().ParameterTypes())
        {
            Report(type, context);
        }
    }

    private static void Report(TypeSyntax? syntax, SyntaxNodeAnalysisContext context)
    {
        foreach (var sub in syntax.SubTypes())
        {
            if (context.SemanticModel.GetTypeInfo(sub).Type is INamedTypeSymbol type
                && type.NotNullable() is { } inner
                && Description(inner) is { } diagnostic)
            {
                context.ReportDiagnostic(diagnostic, sub);
            }
        }
    }

    private static DiagnosticDescriptor? Description(INamedTypeSymbol type)
        => EnumDefaultIsNoneOrEmpty(type) ?? DefaultIsEmpty(type);

    private static DiagnosticDescriptor? EnumDefaultIsNoneOrEmpty(INamedTypeSymbol type)
    {
        return type.TypeKind == TypeKind.Enum && type.GetMembers().Any(IsNoneOrEmpty)
            ? Rule.DefineEnumPropertiesAsNotNullable
            : null;

        static bool IsNoneOrEmpty(ISymbol member)
             => member is IFieldSymbol field
             && Convert.ToInt64(field.ConstantValue) == 0
             && ("NONE".Matches(field.Name) || "EMPTY".Matches(field.Name));
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
