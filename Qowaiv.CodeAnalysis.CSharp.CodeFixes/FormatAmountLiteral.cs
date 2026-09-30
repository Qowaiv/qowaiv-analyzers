using System.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Qowaiv.CodeAnalysis.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public sealed class FormatAmountLiteral() : CodeFix(Rule.FormatAmountLiterals.Id)
{
    /// <inheritdoc />
    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        if (await context.ChangeDocumentContext() is { Node: LiteralExpressionSyntax node } changeDoc)
        {
            changeDoc.RegisterFix("Format amount", context, d => Change(node, d));
        }
    }

    private static async Task<Document> Change(LiteralExpressionSyntax literal, ChangeDocumentContext context)
    {
        return context.Document;
    }
}
