namespace Qowaiv.CodeAnalysis.Syntax;

public abstract class SyntaxAbstraction
{
    /// <summary>Creates a new instance of the <see cref="SyntaxAbstraction"/> class.</summary>
    protected SyntaxAbstraction(SyntaxNode node) => Node = Guard.NotNull(node, nameof(node));

    /// <summary>Gets the underlying node.</summary>
    public SyntaxNode Node { get; }

    /// <summary>
    /// The node that contains this node in its <see cref="SyntaxNode.ChildNodes"/> collection.
    /// </summary>
    public SyntaxNode Parent => Node.Parent;

    /// <summary>Gets the Name of the node or null if not supported for the syntax node type.</summary>
    public string Name() => Node.Name();

    /// <inheritdoc />
    public override string ToString() => Node.ToString();

    /// <summary>Implicitly casts to a <see cref="SyntaxNode"/>.</summary>
    public static implicit operator SyntaxNode(SyntaxAbstraction abstraction) => abstraction?.Node;
}
