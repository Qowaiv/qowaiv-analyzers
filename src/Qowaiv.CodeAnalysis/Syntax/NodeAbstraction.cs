using Microsoft.CodeAnalysis;

namespace Qowaiv.CodeAnalysis.Syntax
{
    public abstract class NodeAbstraction
    {
        /// <summary>Creates a new instance of the <see cref="NodeAbstraction"/> class.</summary>
        protected NodeAbstraction(SyntaxNode node) => Node = Guard.NotNull(node, nameof(node));
        
        /// <summary>Gets the underlying node.</summary>
        public SyntaxNode Node { get; }

        /// <summary>Gets the Name of the node or null if not supported for the syntax node type.</summary>
        public string Name() => Node.Name();

        /// <summary>Implicitly casts to a <see cref="SyntaxNode"/>.</summary>
        public static implicit operator SyntaxNode(NodeAbstraction abstraction) => abstraction?.Node;
    }
}
