using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Qowaiv.CodeAnalysis.Syntax;
using VB = Qowaiv.CodeAnalysis.VisualBasic.Syntax;

namespace Qowaiv.CodeAnalysis.VisualBasic
{
    internal sealed class VisualBasicNodes : SyntaxNodes
    {
        public override string Language => LanguageNames.VisualBasic;

        public override string Name(SyntaxNode node)
            => node switch
            {
                IdentifierNameSyntax identifier => identifier.Identifier.Text,
                InvocationExpressionSyntax invocation => Name(invocation.Expression),
                MemberAccessExpressionSyntax memberAccess => Name(memberAccess.Name),
                SimpleNameSyntax simpleName => simpleName.Identifier.Text,
                _ => null,
            };

        public override InvocationExpression InvocationExpression(SyntaxNode node) => new VB.InvocationExpression(node);
    }
}
