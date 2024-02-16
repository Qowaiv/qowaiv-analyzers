namespace Microsoft.CodeAnalysis;

public static class SyntaxNodeExtensions
{
    public static TNode? AncestorsAndSelf<TNode>(this SyntaxNode node) where TNode : SyntaxNode
        => node.AncestorsAndSelf().OfType<TNode>().FirstOrDefault();

    public static string? Name(this SyntaxNode node) => node switch
    {
        AttributeSyntax attr => Name(attr.Name),
        IdentifierNameSyntax identifier => identifier.Identifier.Text,
        InvocationExpressionSyntax invocation => Name(invocation.Expression),
        MemberAccessExpressionSyntax memberAccess => Name(memberAccess.Name),
        SimpleNameSyntax simpleName => simpleName.Identifier.Text,
        NameSyntax name => name.ToFullString(),
        _ => null,
    };

    public static TNode Cast<TNode>(this SyntaxNode node) where TNode : SyntaxNode
        => node as TNode
        ?? throw new InvalidOperationException($"Unexpected {node.GetType().Name}, expected {typeof(TNode).Name}.");

    public static InvocationExpression InvocationExpression(this SyntaxNode node) => new(node);

    public static TypeDeclaration TypeDeclaration(this SyntaxNode node, SemanticModel model) => node switch
    {
        ClassDeclarationSyntax @class => new TypeDeclaration.Class(@class, model),
        RecordDeclarationSyntax record => new TypeDeclaration.Record(record, model),
        _ => throw new InvalidOperationException($"Unexpected {node.GetType().Name}, expected ClassDeclarationSyntax or RecordDeclarationSyntax."),
    };
}
