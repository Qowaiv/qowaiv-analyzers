namespace Qowaiv.CodeAnalysis;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UseTestableTimeProvider : DiagnosticAnalyzer
{
    public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => Description.UseTestableTimeProvider.Array();

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(Report, SyntaxKind.IdentifierName);
    }

    private void Report(SyntaxNodeAnalysisContext context)
    {
        if (IsDateTimeProvider(context.Node.Name())
            && context.SemanticModel.GetSymbolInfo(context.Node).Symbol is IPropertySymbol property
            && property.MemberOf(SystemType.System_DateTime))
        {
            context.ReportDiagnostic(Description.UseTestableTimeProvider, context.Node.Parent);
        }
    }

    private bool IsDateTimeProvider(string name)
       => nameof(DateTime.Now).Equals(name)
       || nameof(DateTime.UtcNow).Equals(name)
       || nameof(DateTime.Today).Equals(name);
}
