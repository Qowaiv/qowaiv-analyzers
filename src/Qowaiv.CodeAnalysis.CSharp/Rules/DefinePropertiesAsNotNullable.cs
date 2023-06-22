namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class DefinePropertiesAsNotNullable : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = Rule.DefinePropertiesAsNotNullable.Array();

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
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
            && type.IsValueType
            && DefaultIsEmpty(type))
        {
            context.ReportDiagnostic(Rule.DefinePropertiesAsNotNullable, syntax);
        }
    }

    private static bool DefaultIsEmpty(INamedTypeSymbol type)
        => type.IsNullableValueType()
        && type.TypeArguments[0] is INamedTypeSymbol tp
        && tp.GetMembers("Empty").Any(IsReadOnlyField);

    private static bool IsReadOnlyField(ISymbol member)
        => member is IFieldSymbol field
        && field.IsReadOnly
        && field.IsStatic
        && field.Type.Equals(field.ContainingType, IncludeNullability: false)
        && field.IsPublic();
}
