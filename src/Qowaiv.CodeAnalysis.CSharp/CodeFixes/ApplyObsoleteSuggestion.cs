using System.Text.RegularExpressions;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Qowaiv.CodeAnalysis.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public sealed class ApplyObsoleteSuggestion() : CodeFix(ExtenalRule.CS0618)
{
    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        if (Suggestion(context.Diagnostics.FirstOrDefault()?.ToString()) is { } suggestion
            && await context.ChangeDocumentContext() is { Node: { } } changeDoc)
        {
            changeDoc.RegisterFix("Use suggested", context, _ => ApplySuggestion(changeDoc, suggestion));
        }
    }

    private static Task<Document> ApplySuggestion(ChangeDocumentContext context, string suggestion) 
        => context.ReplaceNode(context.Node!, IdentifierName(suggestion));

    [Pure]
    public static string? Suggestion(string? obsoleteMessage)
        => obsoleteMessage is { } && Pattern.Match(obsoleteMessage) is { Success: true } match
            ? match.Groups[nameof(Suggestion)].Value
            : null;

    private static readonly Regex Pattern = new(
        @"Use (?<Suggestion>.+?) instead\.",
        RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture | RegexOptions.Compiled,
        TimeSpan.FromMilliseconds(100));

}
