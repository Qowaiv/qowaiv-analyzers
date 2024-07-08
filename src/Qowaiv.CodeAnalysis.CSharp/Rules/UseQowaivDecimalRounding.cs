namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UseQowaivDecimalRounding() : CodingRule(Rule.UseQowaivDecimalRounding)
{
    protected override void Register(AnalysisContext context)
        => context.RegisterSyntaxNodeAction(Report, SyntaxKind.IdentifierName);

    private void Report(SyntaxNodeAnalysisContext context)
    {
        if (IsMathRound(context.Node.Name())
            && context.SemanticModel.GetSymbolInfo(context.Node).Symbol is IMethodSymbol method
            && method.ReceiverType.Is(SystemType.System_Math)
            && method.ReturnType.Is(SystemType.System_Decimal))
        {
            context.ReportDiagnostic(Diagnostic, context.Node.Parent!.Parent!);
        }
    }

    private static bool IsMathRound(string? name)
        => nameof(Math.Round).Equals(name)
        || nameof(Math.Truncate).Equals(name)
        || nameof(Math.Ceiling).Equals(name)
        || nameof(Math.Floor).Equals(name);
}
