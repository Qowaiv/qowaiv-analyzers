using Microsoft.CodeAnalysis;

namespace Qowaiv.CodeAnalysis.Syntax
{
    public abstract class SyntaxAbstraction
    {
        public abstract string Language { get; }

        public abstract string Name(SyntaxNode node);
        public abstract InvocationExpression InvocationExpression(SyntaxNode node);
    }
}
