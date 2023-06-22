namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UseTestableTimeProvider : CodingRule
{
    public UseTestableTimeProvider() : base(Rule.UseTestableTimeProvider) { }

    protected override void Register(AnalysisContext context)
        => context.RegisterSyntaxNodeAction(Report, SyntaxKind.IdentifierName);

    private void Report(SyntaxNodeAnalysisContext context)
    {
        if (IsDateTimeProvider(context.Node.Name())
            && context.SemanticModel.GetSymbolInfo(context.Node).Symbol is IPropertySymbol property
            && (property.MemberOf(SystemType.System_DateTime) || property.MemberOf(SystemType.System_DateTimeOffset)))
        {
            context.ReportDiagnostic(Diagnostic, context.Node.Parent!);
        }
    }

    private static bool IsDateTimeProvider(string? name)
        => nameof(DateTime.Now).Equals(name)
        || nameof(DateTime.UtcNow).Equals(name)
        || nameof(DateTime.Today).Equals(name);
}
