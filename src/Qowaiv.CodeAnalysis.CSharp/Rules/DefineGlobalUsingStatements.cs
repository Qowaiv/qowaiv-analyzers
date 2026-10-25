namespace Qowaiv.CodeAnalysis.Rules;

public abstract class DefineGlobalUsingStatements(DescriptorContainer supportedDiagnostic)
    : CodingRule(supportedDiagnostic)
{
    protected override void Register(AnalysisContext context)
        => RegisterSyntaxNodeAction(context, Report, SyntaxKind.UsingDirective);

    protected abstract void Report(SyntaxNodeAnalysisContext context);

    protected static bool IsGlobalDirective(SyntaxNode node)
       => node is UsingDirectiveSyntax direcive
       && direcive.ChildTokens().Any(token => token.IsKind(SyntaxKind.GlobalKeyword));
}
