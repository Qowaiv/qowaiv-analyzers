using Microsoft.CodeAnalysis;

namespace Qowaiv.CodeAnalysis.Syntax
{
    public abstract class Identifier
    {
        protected Identifier(SyntaxNode node) => Node = node;
        public SyntaxNode Node { get; }

        public abstract string Name { get; }
    }
}
