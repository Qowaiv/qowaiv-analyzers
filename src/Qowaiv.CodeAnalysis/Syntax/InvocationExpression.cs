using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Qowaiv.CodeAnalysis.Syntax
{
    public abstract class InvocationExpression : NodeAbstraction
    {
        protected InvocationExpression(SyntaxNode node) : base(node) { }

        public abstract IEnumerable<SyntaxNode> Arguments { get; }
        public abstract IEnumerable<SyntaxNode> Expressions { get; }
    }
}
