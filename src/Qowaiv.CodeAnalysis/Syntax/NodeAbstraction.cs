using Microsoft.CodeAnalysis;

namespace Qowaiv.CodeAnalysis.Syntax
{
    public abstract class NodeAbstraction
    {
        protected NodeAbstraction(SyntaxNode node) => Node = Guard.NotNull(node, nameof(node));
        public SyntaxNode Node { get; }
    }
}
