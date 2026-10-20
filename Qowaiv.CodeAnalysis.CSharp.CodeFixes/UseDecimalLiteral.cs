namespace Qowaiv.CodeAnalysis.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public sealed class UseDecimalLiteral() : CodeFix(Rule.RoundDoubleAfterConversionToDecimal.Id)
{
    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        if (await context.ChangeDocumentContext() is { Node: { } node } change
            && Literal(node) is { } literal)
        {
            change.RegisterFix("Use decimal literal", context, d => Change(node, literal, d));
        }
    }

    [Pure]
    private static Task<Document> Change(SyntaxNode node, LiteralExpressionSyntax literal, ChangeDocumentContext context)
    {
        var updated = literal.WithToken(SyntaxFactory.Literal(DecimalTokenText(literal), decimal.Parse(literal.Token.ValueText, CultureInfo.InvariantCulture)));
        return context.ReplaceNode(node, updated.WithTriviaFrom(node));
    }

    [Pure]
    private static string DecimalTokenText(LiteralExpressionSyntax literal)
    {
        var text = literal.Token.Text;
        return text.EndsWith("d", StringComparison.OrdinalIgnoreCase)
            ? $"{text[..^1]}m"
            : $"{text}m";
    }

    [Pure]
    private static LiteralExpressionSyntax? Literal(SyntaxNode node) => node switch
    {
        LiteralExpressionSyntax literal => literal,
        CastExpressionSyntax cast => Literal(cast.Expression),
        BaseObjectCreationExpressionSyntax creation when creation.ArgumentList is { Arguments.Count: 1 } list
            => list.Arguments[0].Expression as LiteralExpressionSyntax,
        InvocationExpressionSyntax invocation when invocation.ArgumentList is { Arguments.Count: 1 } list
            => list.Arguments[0].Expression as LiteralExpressionSyntax,
        _ => null,
    };
}
