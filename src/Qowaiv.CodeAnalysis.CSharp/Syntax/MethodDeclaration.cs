namespace Qowaiv.CodeAnalysis.Syntax;

public sealed class MethodDeclaration : SyntaxAbstraction<IMethodSymbol>
{
    public MethodDeclaration(BaseMethodDeclarationSyntax methodNode, SemanticModel semanticModel) : base(methodNode, semanticModel)
    {
        TypedNode = methodNode;
    }

    public BaseMethodDeclarationSyntax TypedNode { get; }

    public IEnumerable<SyntaxKind> Modifiers => TypedNode.Modifiers.Select(m => m.Kind());

    public IEnumerable<AttributeDecoration> Attributes => TypedNode.AttributeLists
       .SelectMany(a => a.Attributes)
       .Select(a => new AttributeDecoration(a, SemanticModel));

    public bool IsPartial => Modifiers.Contains(SyntaxKind.PartialKeyword);

    public bool IsObsolete => Attributes.Any(a => a.IsObsoleteAttribute)
            || (IsPartial && Symbol is { IsObsolete: true });

    [Pure]
    protected override IMethodSymbol? GetSymbol(SemanticModel semanticModel)
        => semanticModel.GetDeclaredSymbol(TypedNode);
}
