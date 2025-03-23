namespace Qowaiv.CodeAnalysis.Rules;

public abstract class DefineGlobalUsingStatements(DiagnosticDescriptor supportedDiagnostic)
    : CodingRule(supportedDiagnostic)
{
    protected override void Register(AnalysisContext context)
        => context.RegisterSyntaxNodeAction(Report, SyntaxKind.UsingDirective);

    protected abstract void Report(SyntaxNodeAnalysisContext context);

    protected static bool IsGlobalDirective(SyntaxNode node)
       => node is UsingDirectiveSyntax direcive
       && direcive.ChildTokens().Any(token => token.IsKind(SyntaxKind.GlobalKeyword));
}
