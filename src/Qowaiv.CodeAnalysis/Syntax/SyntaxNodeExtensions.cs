namespace Qowaiv.CodeAnalysis.Syntax;

public static class SyntaxNodeExensions
{
    public static TNode Cast<TNode>(this SyntaxNode node) where TNode : SyntaxNode
        => node as TNode
        ?? throw new InvalidOperationException($"Unexpected {node.GetType().Name}, expected {typeof(TNode).Name}.");
    public static InvocationExpression InvocationExpression(this SyntaxNode node)
      => new(node);
}
