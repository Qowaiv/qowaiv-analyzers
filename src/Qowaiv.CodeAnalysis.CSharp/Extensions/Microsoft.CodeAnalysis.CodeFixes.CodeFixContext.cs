using Qowaiv.CodeAnalysis.CodeFixes;

namespace Microsoft.CodeAnalysis.CodeFixes;

/// <summary>Extensions on <see cref="CodeFixContext"/>.</summary>
internal static class CodeFixContextExtensions
{
    public static async Task<ChangeDocumentContext?> ChangeDocumentContext(this CodeFixContext context)
        => context.Diagnostics.FirstOrDefault() is { } diagnostic
        && (await context.Document.GetSyntaxRootAsync(context.CancellationToken)) is { } root
            ? new(context.Document, diagnostic, root, context.CancellationToken)
            : null;
}
