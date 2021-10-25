using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Qowaiv.CodeAnalysis.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Qowaiv.CodeAnalysis.VisualBasic.Syntax
{
    internal sealed class InvocationExpression : CodeAnalysis.Syntax.InvocationExpression
    {
        public InvocationExpression(SyntaxNode node) : base(node) { }

        public override IEnumerable<SyntaxNode> Arguments
           => Node.Cast<InvocationExpressionSyntax>().ArgumentList?.Arguments
           ?? Enumerable.Empty<SyntaxNode>();

        public override IEnumerable<SyntaxNode> Expressions
          => Node.Cast<InvocationExpressionSyntax>().ArgumentList?.Arguments.Select(arg => arg.GetExpression())
          ?? Enumerable.Empty<SyntaxNode>();
    }
}
