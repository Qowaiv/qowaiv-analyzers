
namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class DefineGlobalUsingStatementsSeparately() : CodingRule(Rule.DefineGlobalUsingStatementsSeparately)
{
    protected override void Register(AnalysisContext context)
        => context.RegisterSyntaxNodeAction(Report, SyntaxKind.UsingDirective);

    private void Report(SyntaxNodeAnalysisContext context)
    {
        if (IsGlobalDirective(context.Node) && !AllGobal(context.Node))
        {
            context.ReportDiagnostic(Diagnostic, context.Node);
        }
    }

    private static bool AllGobal(SyntaxNode node)
        => node.Parent is CompilationUnitSyntax root
        && root.ChildNodes().All(IsGlobalDirective);

    private static bool IsGlobalDirective(SyntaxNode node)
        => node is UsingDirectiveSyntax direcive
        && direcive.ChildTokens().Any(token => token.IsKind(SyntaxKind.GlobalKeyword));
}
