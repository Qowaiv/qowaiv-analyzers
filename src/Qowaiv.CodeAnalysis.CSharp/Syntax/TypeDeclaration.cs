namespace Qowaiv.CodeAnalysis.Syntax;

public abstract partial class TypeDeclaration(SyntaxNode node, SemanticModel semanticModel)
    : SyntaxAbstraction<INamedTypeSymbol>(node, semanticModel)
{
    public abstract SyntaxList<AttributeListSyntax> AttributeLists { get; }

    public abstract SyntaxToken Identifier { get; }

    public IEnumerable<AttributeDecoration> Attributes => AttributeLists
        .SelectMany(a => a.Attributes)
        .Select(a => new AttributeDecoration(a, SemanticModel));

    public bool IsConcrete => !IsAbstract && !IsStatic;

    public bool IsStatic
        => Modifiers.Contains(SyntaxKind.StaticKeyword)
        || (IsPartial && Symbol?.IsStatic is true);

    public bool IsSealed
        => Modifiers.Contains(SyntaxKind.SealedKeyword)
        || (IsPartial && Symbol?.IsSealed is true);

    public Accessibility Accessibility
        => IsPartial && Symbol is { } s
            ? s.DeclaredAccessibility
            : Modifiers.GetAccessibility();

    public bool IsAbstract
        => Modifiers.Contains(SyntaxKind.AbstractKeyword)
        || (IsPartial && Symbol?.IsAbstract is true);

    public bool IsPartial => Modifiers.Contains(SyntaxKind.PartialKeyword);

    public bool IsPublic
        => Modifiers.Contains(SyntaxKind.PublicKeyword)
        || (IsPartial && Symbol?.IsPublic() is true);

    public bool IsObsolete
        => (IsPartial || Attributes.Any())
        && Symbol is { } symbol
        && symbol.IsObsolete();

    public abstract IEnumerable<SyntaxKind> Modifiers { get; }

    [Pure]
    protected override INamedTypeSymbol? GetSymbol(SemanticModel semanticModel)
        => semanticModel.GetDeclaredSymbol(Node) as INamedTypeSymbol;
}
