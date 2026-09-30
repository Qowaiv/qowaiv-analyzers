using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Qowaiv.CodeAnalysis.CodeFixes;

/// <summary>Implement fix for <see cref="Rule.UseQowaivClockTimeProvider"/>.</summary>
[ExportCodeFixProvider(LanguageNames.CSharp)]
public sealed class ChangeToQowaivClockTimeProvider() : CodeFix(Rule.UseQowaivClockTimeProvider.Id)
{
    /// <inheritdoc />
    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        if (await context.ChangeDocumentContext() is { Node: ExpressionSyntax expression } change)
        {
            change.RegisterFix("Use Qowaiv.Clock.TimeProvider", context, c => ChangeDocument(expression, c));
        }
    }

    private static Task<Document> ChangeDocument(ExpressionSyntax expression, ChangeDocumentContext context)
        => context.ReplaceNode(expression, InvocationExpression(IdentifierName("Clock.TimeProvider")));
}
