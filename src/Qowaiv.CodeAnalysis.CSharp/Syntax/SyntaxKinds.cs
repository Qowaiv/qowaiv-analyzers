using Microsoft.CodeAnalysis.CSharp;
using Qowaiv.CodeAnalysis.Syntax;

namespace Qowaiv.CodeAnalysis.CSharp.Syntax
{
    internal sealed class SyntaxKinds : SyntaxKinds<SyntaxKind>
    {
        public override SyntaxKind IdentifierName => SyntaxKind.IdentifierName;
    }
}
