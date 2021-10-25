using Microsoft.CodeAnalysis;
using Qowaiv.CodeAnalysis.Syntax;
using CS = Qowaiv.CodeAnalysis.CSharp.Syntax;

namespace Qowaiv.CodeAnalysis.CSharp
{
    internal sealed class CSharpNodes : SyntaxNodes
    {
        public override string Language => LanguageNames.CSharp;

        public override Identifier Identifier(SyntaxNode node) => new CS.Identifier(node);
    }
}
