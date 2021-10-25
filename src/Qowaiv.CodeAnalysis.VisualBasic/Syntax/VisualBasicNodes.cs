using Microsoft.CodeAnalysis;
using Qowaiv.CodeAnalysis.Syntax;
using VB = Qowaiv.CodeAnalysis.VisualBasic.Syntax;

namespace Qowaiv.CodeAnalysis.VisualBasic
{
    internal sealed class VisualBasicNodes : SyntaxNodes
    {
        public override string Language => LanguageNames.VisualBasic;

        public override Identifier Identifier(SyntaxNode node) => new VB.Identifier(node);
    }
}
