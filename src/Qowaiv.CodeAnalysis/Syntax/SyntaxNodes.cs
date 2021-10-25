using Microsoft.CodeAnalysis;

namespace Qowaiv.CodeAnalysis.Syntax
{
    public abstract class SyntaxNodes
    {
        public abstract string Language { get; }

        public abstract Identifier Identifier(SyntaxNode node);
    }
}
