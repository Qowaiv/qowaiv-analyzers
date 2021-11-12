namespace Qowaiv.CodeAnalysis.Syntax;

internal static class VisualNodeExtensions
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
