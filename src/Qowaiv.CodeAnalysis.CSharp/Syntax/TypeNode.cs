namespace Qowaiv.CodeAnalysis.Syntax;

public partial class TypeNode(TypeSyntax typeNode, SemanticModel semanticModel)
    : SyntaxAbstraction<INamedTypeSymbol>(typeNode, semanticModel)
{
    private readonly TypeSyntax TypedNode = typeNode;

    public virtual bool IsArray => false;

    public virtual bool IsGenericType => false;

    public virtual IReadOnlyList<TypeNode> GenericTypeArguments { get; } = [];

    public virtual TypeNode? ElementType => null;

    [Pure]
    protected override INamedTypeSymbol? GetSymbol(SemanticModel semanticModel)
        => semanticModel.GetTypeInfo(TypedNode).Type as INamedTypeSymbol;
}
