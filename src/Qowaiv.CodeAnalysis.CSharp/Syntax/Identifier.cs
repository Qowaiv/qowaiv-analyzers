using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Qowaiv.CodeAnalysis.Syntax;

namespace Qowaiv.CodeAnalysis.CSharp.Syntax
{
    internal class Identifier : CodeAnalysis.Syntax.Identifier
    {
        public Identifier(SyntaxNode node) : base(node) { }

        public override string Name => Node.Cast<IdentifierNameSyntax>().Identifier.Text;
    }
}
