namespace Qowaiv.CodeAnalysis.Syntax;

public sealed class InvocationExpression : SyntaxAbstraction
{
    public InvocationExpression(SyntaxNode node) : base(node) { }

    public IEnumerable<SyntaxNode> Arguments
       => Node.Cast<InvocationExpressionSyntax>().ArgumentList?.Arguments
       ?? Enumerable.Empty<SyntaxNode>();

    public IEnumerable<SyntaxNode> Expressions
      => Node.Cast<InvocationExpressionSyntax>().ArgumentList?.Arguments.Select(arg => arg.Expression)
      ?? Enumerable.Empty<SyntaxNode>();
}
