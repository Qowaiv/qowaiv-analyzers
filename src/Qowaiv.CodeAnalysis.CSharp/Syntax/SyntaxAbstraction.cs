namespace Qowaiv.CodeAnalysis.Syntax;

public abstract class SyntaxAbstraction<TSymbol> where TSymbol : ISymbol
{
    /// <summary>Initializes a new instance of the <see cref="SyntaxAbstraction{TSymbol}"/> class.</summary>
    protected SyntaxAbstraction(SyntaxNode node, SemanticModel semanticModel)
    {
        Node = Guard.NotNull(node, nameof(node));
        SemanticModel = Guard.NotNull(semanticModel, nameof(semanticModel));
        LazySymbol = new(() => GetSymbol(SemanticModel));
    }

    [Pure]
    protected abstract TSymbol? GetSymbol(SemanticModel semanticModel);

    public TSymbol? Symbol => LazySymbol.Value;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Lazy<TSymbol?> LazySymbol;

    /// <summary>Gets the underlying node.</summary>
    public SyntaxNode Node { get; }

    /// <summary>Gets the syntax kind of the node.</summary>
    public SyntaxKind Kind => Node.Kind();

    /// <summary>The underlying semantic model.</summary>
    protected SemanticModel SemanticModel { get; }

    /// <summary>
    /// The node that contains this node in its <see cref="SyntaxNode.ChildNodes"/> collection.
    /// </summary>
    public SyntaxNode? Parent => Node.Parent;

    /// <summary>Gets the Name of the node or null if not supported for the syntax node type.</summary>
    public string Name() => Node.Name() ?? string.Empty;

    /// <summary>Gets the direct child tokens of this node.</summary>
    public IEnumerable<SyntaxToken> ChildTokens() => Node.ChildTokens();

    /// <inheritdoc />
    public override string ToString() => Node.ToString();

    /// <inheritdoc cref="Microsoft.CodeAnalysis.CSharpExtensions.IsKind(SyntaxNode?, SyntaxKind)"/>
    [Pure]
    public bool IsKind(SyntaxKind kind) => Node.IsKind(kind);

    /// <summary>Implicitly casts to a <see cref="SyntaxNode"/>.</summary>
    public static implicit operator SyntaxNode(SyntaxAbstraction<TSymbol> abstraction) => abstraction.Node;
}
