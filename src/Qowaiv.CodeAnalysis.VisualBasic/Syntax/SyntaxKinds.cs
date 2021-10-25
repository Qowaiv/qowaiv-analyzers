using Microsoft.CodeAnalysis.VisualBasic;
using Qowaiv.CodeAnalysis.Syntax;

namespace Qowaiv.CodeAnalysis.VisualBasic.Syntax
{
    internal sealed class SyntaxKinds : SyntaxKinds<SyntaxKind>
    {
        public override SyntaxKind IdentifierName => SyntaxKind.IdentifierName;
        public override SyntaxKind InvocationExpression => SyntaxKind.InvocationExpression;
    }
}
