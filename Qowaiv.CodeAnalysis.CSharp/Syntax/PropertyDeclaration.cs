namespace Qowaiv.CodeAnalysis.Syntax;

public sealed class PropertyDeclaration : SyntaxAbstraction<IPropertySymbol>
{
    public PropertyDeclaration(PropertyDeclarationSyntax propertyNode, SemanticModel semanticModel) : base(propertyNode, semanticModel)
    {
        TypedNode = propertyNode;
        LazyContainingSymbol = new(() => Symbol?.ContainingSymbol as INamedTypeSymbol);
    }

    public PropertyDeclarationSyntax TypedNode { get; }

    public bool IsInstance => !IsStatic;

    public bool IsStatic => Modifiers.Contains(SyntaxKind.StaticKeyword);

    public bool IsOverride => Modifiers.Contains(SyntaxKind.OverrideKeyword);

    public INamedTypeSymbol? ContainingSymbol => LazyContainingSymbol.Value;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Lazy<INamedTypeSymbol?> LazyContainingSymbol;

    public IEnumerable<SyntaxKind> Modifiers => TypedNode.Modifiers.Select(m => m.Kind());

    public Accessibility Accessibility
        => DeclaringType.IsKind(SyntaxKind.InterfaceDeclaration)
            ? Modifiers.GetAccessibility(Accessibility.Public)
            : Modifiers.GetAccessibility();

    public IEnumerable<SyntaxKind> Accessors
        => TypedNode.AccessorList?.Accessors.Select(s => s.Kind()) ?? [];

    public IEnumerable<AttributeDecoration> Attributes => TypedNode.AttributeLists
       .SelectMany(a => a.Attributes)
       .Select(a => new AttributeDecoration(a, SemanticModel));

    public bool IsObsolete
        => Symbol is { } symbol
        && symbol.IsObsolete();

    public TypeNode PropertyType => field ??= TypedNode.Type.TypeNode(SemanticModel);

    public TypeDeclaration DeclaringType
        => field
        ??= TypedNode
            .Ancestors()
            .Select(node => node.TryTypeDeclaration(SemanticModel))
            .OfType<TypeDeclaration>()
            .First();

    [Pure]
    protected override IPropertySymbol? GetSymbol(SemanticModel semanticModel)
        => semanticModel.GetDeclaredSymbol(TypedNode);
}
