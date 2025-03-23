using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Qowaiv.CodeAnalysis.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public sealed class ChangeQowaivDateToSystemDateOnly() : CodeFix(Rule.UseSystemDateOnly.Id)
{
    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        if (await context.ChangeDocumentContext() is { Node: { } } change)
        {
            change.RegisterFix("Change type to System.DateOnly.", context, ChangeDocument);
        }
    }

    private static Task<Document> ChangeDocument(ChangeDocumentContext context)
        => context.ReplaceNode(
            Identifier(context.Node) ?? context.Node!,
            IdentifierName("DateOnly"));

    private static SyntaxNode? Identifier(SyntaxNode? node) => node switch
    {
        null => null,
        IdentifierNameSyntax name => name,
        NullableTypeSyntax nullable => nullable.ElementType,
        _ => Identifier(node.Parent),
    };
}
