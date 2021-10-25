using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Qowaiv.CodeAnalysis.Syntax;
using CS = Qowaiv.CodeAnalysis.CSharp.Syntax;

namespace Qowaiv.CodeAnalysis.CSharp
{
    internal sealed class CSharpAbstraction : SyntaxAbstraction
    {
        public override string Language => LanguageNames.CSharp;

        public override string Name(SyntaxNode node)
           => node switch
           {
               IdentifierNameSyntax identifier => identifier.Identifier.Text,
               InvocationExpressionSyntax invocation => Name(invocation.Expression),
               MemberAccessExpressionSyntax memberAccess => Name(memberAccess.Name),
               SimpleNameSyntax simpleName => simpleName.Identifier.Text,
               _ => null,
           };

        public override InvocationExpression InvocationExpression(SyntaxNode node) => new CS.InvocationExpression(node);
    }
}
