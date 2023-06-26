﻿using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Qowaiv.CodeAnalysis.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public sealed class SealClass : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds => new[]
    {
        Rule.SealClasses.Id,
    }
    .ToImmutableArray();

    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        if (await context.ChangeDocumentContext() is { } changeDoc)
        {
            if (changeDoc.Node is ClassDeclarationSyntax @class)
            {
                changeDoc.RegisterCodeFix("Seal class.", context, d => ChangeClass(@class, d));
            }
            else if (changeDoc.Node is RecordDeclarationSyntax @record)
            {
                changeDoc.RegisterCodeFix("Seal record.", context, d => ChangeRecord(@record, d));
            }
        }
    }

    private static Task<Document> ChangeClass(ClassDeclarationSyntax @class, ChangeDocumentContext context)
    {
        var modifiers = @class.Modifiers.Add(Token(SyntaxKind.SealedKeyword));
        var newNode = @class.WithModifiers(modifiers);
        return context.ReplaceNode(@class, newNode);
    }

    private static Task<Document> ChangeRecord(RecordDeclarationSyntax record, ChangeDocumentContext context)
    {
        var modifiers = record.Modifiers.Add(Token(SyntaxKind.SealedKeyword));
        var newNode = record.WithModifiers(modifiers);
        return context.ReplaceNode(record, newNode);
    }
}
