namespace Qowaiv.CodeAnalysis.Syntax;

public sealed class PropertyDeclaration : SyntaxAbstraction<IPropertySymbol>
{
    public PropertyDeclaration(PropertyDeclarationSyntax propertyNode, SemanticModel semanticModel) : base(propertyNode, semanticModel)
    {
        TypedNode = propertyNode;
        LazyContainingSymbol = new(() => Symbol?.ContainingSymbol as INamedTypeSymbol);
    }

    public PropertyDeclarationSyntax TypedNode { get; }

    public bool IsStatic => Modifiers.Contains(SyntaxKind.StaticKeyword);

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

    public bool IsObsolete
        => Symbol is { } symbol
        && symbol.IsObsolete();

    public TypeNode PropertyType => propertyType ??= TypedNode.Type.TypeNode(SemanticModel);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private TypeNode? propertyType;

    public TypeDeclaration DeclaringType
        => declaringType ??= TypedNode
            .Ancestors()
            .Select(node => node.TryTypeDeclaration(SemanticModel))
            .OfType<TypeDeclaration>()
            .First();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private TypeDeclaration? declaringType;

    [Pure]
    protected override IPropertySymbol? GetSymbol(SemanticModel semanticModel)
        => semanticModel.GetDeclaredSymbol(TypedNode);
}
