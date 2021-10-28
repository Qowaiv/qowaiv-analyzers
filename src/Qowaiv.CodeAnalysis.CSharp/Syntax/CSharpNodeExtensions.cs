using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Qowaiv.CodeAnalysis.Syntax
{
    public static class CSharpNodeExtensions
    {
        public static string Name(this SyntaxNode node)
           => node switch
           {
               IdentifierNameSyntax identifier => identifier.Identifier.Text,
               InvocationExpressionSyntax invocation => Name(invocation.Expression),
               MemberAccessExpressionSyntax memberAccess => Name(memberAccess.Name),
               SimpleNameSyntax simpleName => simpleName.Identifier.Text,
               _ => null,
           };
    }
}
