using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Qowaiv.CodeAnalysis.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public sealed class AddTrailingZerosToDates() : CodeFix(Rule.UseLeadingZerosToDefineDateConstants.Id)
{
    /// <inheritdoc />
    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        if (await context.ChangeDocumentContext() is { } changeDoc && changeDoc.Node is ArgumentSyntax arg)
        {
            // We use the provided message to get the required number of leading zeros.
            var title = context.Diagnostics[0].GetMessage().Substring(0, 19).Trim();
            var count = title[4] - '0';
            changeDoc.RegisterFix(title, context, doc => Change(arg, count, doc));
        }
    }

    private static async Task<Document> Change(ArgumentSyntax arg, int count, ChangeDocumentContext context)
    {
        var literal = arg.Expression.Cast<LiteralExpressionSyntax>();
        var token = Literal(new string('0', count) + literal.Token.Text, (int)literal.Token.Value!);
        var updated = literal.WithToken(token);
        return await context.ReplaceNode(literal, updated);
    }
}
