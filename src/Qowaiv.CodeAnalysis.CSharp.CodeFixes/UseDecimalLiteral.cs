using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Qowaiv.CodeAnalysis.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public sealed class UseDecimalLiteral() : CodeFix(Rule.RoundDoubleAfterConversionToDecimal.Id)
{
    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        if (await context.ChangeDocumentContext() is { Node: { } node } change && Strip(node) is { } literal)
            change.RegisterFix("Use decimal literal", context, d => Change(node, literal, d));
    }

    [Pure]
    private static Task<Document> Change(SyntaxNode node, LiteralExpressionSyntax literal, ChangeDocumentContext context)
    {
        var token = Trim(literal.Token.Text);
        var updated = literal.WithToken(Literal(token, (decimal)ParseToken(token).Value!));
        return context.ReplaceNode(node, updated.WithTriviaFrom(node));
    }

    [Pure]
    private static string Trim(string text)
        => text[^1] is 'd' or 'D' ? $"{text[..^1]}m" : $"{text}m";

    [Pure]
    private static LiteralExpressionSyntax? Strip(SyntaxNode node) => node switch
    {
        CastExpressionSyntax cast
            => Strip(cast.Expression),
        BaseObjectCreationExpressionSyntax { ArgumentList.Arguments: { Count: 1 } args }
            => args[0].Expression as LiteralExpressionSyntax,
        InvocationExpressionSyntax { ArgumentList.Arguments: { Count: 1 } args }
            => args[0].Expression as LiteralExpressionSyntax,
        _ => node as LiteralExpressionSyntax,
    };
}
