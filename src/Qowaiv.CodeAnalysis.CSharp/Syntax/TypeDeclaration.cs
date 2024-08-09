namespace Qowaiv.CodeAnalysis.Syntax;

public abstract partial class TypeDeclaration : SyntaxAbstraction<INamedTypeSymbol>
{
    protected TypeDeclaration(SyntaxNode node, SemanticModel semanticModel)
        : base(node, semanticModel)
    {
    }

    public abstract SyntaxList<AttributeListSyntax> AttributeLists { get; }

    public IEnumerable<AttributeSyntax> Attributes => AttributeLists.SelectMany(a => a.Attributes);

    public bool IsConcrete => !IsAbstract && !IsStatic;

    public bool IsStatic
        => Modifiers.Contains(SyntaxKind.StaticKeyword)
        || (IsPartial && Symbol?.IsStatic == true);

    public bool IsSealed
        => Modifiers.Contains(SyntaxKind.SealedKeyword)
        || (IsPartial && Symbol?.IsSealed == true);

    public Accessibility Accessibility
        => IsPartial && Symbol is { } s
            ? s.DeclaredAccessibility
            : Modifiers.GetAccessibility();

    public bool IsAbstract
        => Modifiers.Contains(SyntaxKind.AbstractKeyword)
        || (IsPartial && Symbol?.IsAbstract == true);

    public bool IsPartial => Modifiers.Contains(SyntaxKind.PartialKeyword);

    public bool IsPublic
        => Modifiers.Contains(SyntaxKind.PublicKeyword)
        || (IsPartial && Symbol?.IsPublic() == true);

    public bool IsObsolete
        => (IsPartial || Attributes.Any())
        && Symbol is { } symbol
        && symbol.IsObsolete();

    public abstract IEnumerable<SyntaxKind> Modifiers { get; }

    [Pure]
    protected override INamedTypeSymbol? GetSymbol(SemanticModel semanticModel)
        => semanticModel.GetDeclaredSymbol(Node) as INamedTypeSymbol;
}
