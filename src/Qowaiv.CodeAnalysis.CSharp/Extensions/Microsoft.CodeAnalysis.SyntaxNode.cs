namespace Microsoft.CodeAnalysis;

public static class SyntaxNodeExtensions
{
    public static TNode? AncestorsAndSelf<TNode>(this SyntaxNode node) where TNode : SyntaxNode
        => node.AncestorsAndSelf().OfType<TNode>().FirstOrDefault();

    [Pure]
    public static string? Name(this SyntaxNode? node) => node switch
    {
        ArgumentSyntax n /*...............*/ => Name(n.Expression),
        AttributeSyntax n /*..............*/ => Name(n.Name),
        FieldDeclarationSyntax n /*.......*/ => Name(n.Declaration),
        IdentifierNameSyntax n /*.........*/ => n.Identifier.Text,
        InvocationExpressionSyntax n /*...*/ => Name(n.Expression),
        MemberAccessExpressionSyntax n /*.*/ => Name(n.Name),
        ParameterSyntax n /*..............*/ => n.Identifier.Text,
        SimpleNameSyntax n /*.............*/ => n.Identifier.Text,
        NameSyntax n /*...................*/ => n.ToFullString(),
        PropertyDeclarationSyntax n /*....*/ => n.Identifier.Text,
        VariableDeclarationSyntax n /*....*/ => Name(n.Variables.FirstOrDefault()),
        VariableDeclaratorSyntax n /*.....*/ => n.Identifier.Text,
        _ => null,
    };

    [Pure]
    public static TNode Cast<TNode>(this SyntaxNode node) where TNode : SyntaxNode
        => node as TNode
        ?? throw new InvalidOperationException($"Unexpected {node.GetType().Name}, expected {typeof(TNode).Name}.");

    [Pure]
    public static PropertyDeclaration PropertyDeclaration(this SyntaxNode node, SemanticModel model)
        => new(node.Cast<PropertyDeclarationSyntax>(), model);

    [Pure]
    public static TypeDeclaration TypeDeclaration(this SyntaxNode node, SemanticModel model)
        => TryTypeDeclaration(node, model)
        ?? throw new InvalidOperationException($"Unexpected {node.GetType().Name}, expected a type declaration.");

    [Pure]
    public static TypeDeclaration? TryTypeDeclaration(this SyntaxNode node, SemanticModel model) => node switch
    {
        ClassDeclarationSyntax @class => new TypeDeclaration.Class(@class, model),
        InterfaceDeclarationSyntax @interface => new TypeDeclaration.Interface(@interface, model),
        RecordDeclarationSyntax record => new TypeDeclaration.Record(record, model),
        StructDeclarationSyntax @struct => new TypeDeclaration.Struct(@struct, model),
        _ => null,
    };

    [Pure]
    public static MemberDeclaration MemberDeclaration(this SyntaxNode node, SemanticModel model)
        => TryMemberDeclaration(node, model)
        ?? throw new InvalidOperationException($"Unexpected {node.GetType().Name}, expected a member declaration.");

    [Pure]
    public static MemberDeclaration? TryMemberDeclaration(this SyntaxNode node, SemanticModel model) => node switch
    {
        FieldDeclarationSyntax field => new MemberDeclaration.Field(field, model),
        ParameterSyntax param => new MemberDeclaration.Parameter(param, model),
        PropertyDeclarationSyntax prop => new MemberDeclaration.Property(prop, model),
        _ => null,
    };

    [Pure]
    public static TypeNode TypeNode(this SyntaxNode node, SemanticModel model)
        => TryTypeNode(node, model)
        ?? throw new InvalidOperationException($"Unexpected {node.GetType().Name}, expected a type declaration.");

    [Pure]
    public static TypeNode? TryTypeNode(this SyntaxNode node, SemanticModel model) => node switch
    {
        ArrayTypeSyntax @array => new TypeNode.Array(@array, model),
        GenericNameSyntax generic => new TypeNode.Generic(generic, model),
        TypeSyntax @type => new TypeNode(type, model),
        _ => null,
    };
}
