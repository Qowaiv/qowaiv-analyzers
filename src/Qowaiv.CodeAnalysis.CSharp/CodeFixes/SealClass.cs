using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Qowaiv.CodeAnalysis.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public sealed class SealClass() : CodeFix(Rule.SealClasses.Id)
{
    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        if (await context.ChangeDocumentContext() is { } changeDoc)
        {
            if (changeDoc.Node is ClassDeclarationSyntax @class)
            {
                changeDoc.RegisterFix("Seal class.", context, d => Change(@class, d));
            }
            else if (changeDoc.Node is RecordDeclarationSyntax @record)
            {
                changeDoc.RegisterFix("Seal record.", context, d => Change(@record, d));
            }
        }
    }

    private static Task<Document> Change(TypeDeclarationSyntax declaration, ChangeDocumentContext context)
    {
        var modifiers = declaration.Modifiers.Add(Token(SyntaxKind.SealedKeyword));
        var newNode = declaration.WithModifiers(modifiers);
        return context.ReplaceNode(declaration, newNode);
    }
}
