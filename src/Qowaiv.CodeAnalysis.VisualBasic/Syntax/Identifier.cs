using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Qowaiv.CodeAnalysis.Syntax;

namespace Qowaiv.CodeAnalysis.VisualBasic.Syntax
{
    internal class Identifier : CodeAnalysis.Syntax.Identifier
    {
        public Identifier(SyntaxNode node) : base(node) { }

        public override string Name => Node.Cast<IdentifierNameSyntax>().Identifier.Text;
    }
}
