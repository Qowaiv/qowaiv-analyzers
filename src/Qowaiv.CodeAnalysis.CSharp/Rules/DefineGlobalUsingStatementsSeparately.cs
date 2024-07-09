namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class DefineGlobalUsingStatementsSeparately() : DefineGlobalUsingStatements(Rule.DefineGlobalUsingStatementsSeparately)
{
    protected override void Report(SyntaxNodeAnalysisContext context)
    {
        if (IsGlobalDirective(context.Node) && !AllGobal(context.Node))
        {
            context.ReportDiagnostic(Diagnostic, context.Node);
        }
    }

    private static bool AllGobal(SyntaxNode node)
        => node.Parent is CompilationUnitSyntax root
        && root.ChildNodes().All(IsGlobalDirective);
}
