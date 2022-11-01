namespace Microsoft.CodeAnalysis;

public static class SyntaxNodeExensions
{
    public static string? Name(this SyntaxNode node)
      => node switch
      {
          IdentifierNameSyntax identifier => identifier.Identifier.Text,
          InvocationExpressionSyntax invocation => Name(invocation.Expression),
          MemberAccessExpressionSyntax memberAccess => Name(memberAccess.Name),
          SimpleNameSyntax simpleName => simpleName.Identifier.Text,
          _ => null,
      };

    public static TNode Cast<TNode>(this SyntaxNode node) where TNode : SyntaxNode
        => node as TNode
        ?? throw new InvalidOperationException($"Unexpected {node.GetType().Name}, expected {typeof(TNode).Name}.");
    
    public static InvocationExpression InvocationExpression(this SyntaxNode node) => new(node);

    public static MethodDeclaration MethodDeclaration(this SyntaxNode node, SemanticModel model)
        => node is ClassDeclarationSyntax || node is RecordDeclarationSyntax
        ? new(node, new(() => model.GetDeclaredSymbol(node) as INamedTypeSymbol))
        : throw new InvalidOperationException($"Unexpected {node.GetType().Name}, expected MethodDeclarationSyntax or RecordDeclarationSyntax.");
}
