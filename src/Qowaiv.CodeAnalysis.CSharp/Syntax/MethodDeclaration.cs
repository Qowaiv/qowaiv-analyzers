namespace Qowaiv.CodeAnalysis.Syntax;

public sealed class MethodDeclaration : SyntaxAbstraction
{
    public MethodDeclaration(SyntaxNode node, Lazy<INamedTypeSymbol?> symbol) : base(node)
    {
        LazySymbol = symbol;
    }

    public INamedTypeSymbol? Symbol => LazySymbol.Value;
    private readonly Lazy<INamedTypeSymbol?> LazySymbol;

    public bool IsConcrete => !IsSealed && !IsAbstract;

    public bool IsSealed
        => Modifiers.Contains(SyntaxKind.SealedKeyword)
        || (IsPartial && Symbol?.IsSealed == true);

    public bool IsAbstract
        => Modifiers.Contains(SyntaxKind.AbstractKeyword)
        || (IsPartial && Symbol?.IsAbstract == true);
    
    public bool IsPartial => Modifiers.Contains(SyntaxKind.PartialKeyword);

    public IEnumerable<SyntaxKind> Modifiers
        => Node switch
        {
            ClassDeclarationSyntax cls => cls.Modifiers.Select(m => m.Kind()),
            RecordDeclarationSyntax rec => rec.Modifiers.Select(m => m.Kind()),
            _ => throw new InvalidOperationException(),
        };
}
