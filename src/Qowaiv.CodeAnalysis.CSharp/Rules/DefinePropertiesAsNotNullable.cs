namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class DefinePropertiesAsNotNullable : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = Rule.DefinePropertiesAsNotNullable.Array();

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(Report, SyntaxKind.PropertyDeclaration);
    }

    private void Report(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is PropertyDeclarationSyntax declaration
            && context.SemanticModel.GetTypeInfo(declaration.Type).Type is INamedTypeSymbol type
            && type.IsValueType
            && DefaultIsEmpty(type))
        {
            context.ReportDiagnostic(Rule.DefinePropertiesAsNotNullable, declaration.Type);
        }
    }

    private bool DefaultIsEmpty(INamedTypeSymbol type)
        => type.IsNullableValueType()
        && type.TypeArguments[0] is INamedTypeSymbol tp
        && tp.GetMembers().Any(HasEmpty);

    public bool HasEmpty(ISymbol member)
        => member is IFieldSymbol field
        && field.IsReadOnly
        && field.IsStatic
        && field.Type.Equals(field.ContainingType, IncludeNullability: false)
        && field.IsPublic()
        && field.Name == "Empty";
}
