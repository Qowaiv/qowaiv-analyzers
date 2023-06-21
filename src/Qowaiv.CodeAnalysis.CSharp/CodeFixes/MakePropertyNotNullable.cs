using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Qowaiv.CodeAnalysis.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public sealed class MakePropertyNotNullable : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds => new[]
    {
        Rule.UseEmptyInsteadOfNullable.Id,
    }
    .ToImmutableArray();

    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        if (await context.DiagnosticContext() is { } diagnostic
            && diagnostic.Token.Parent!.AncestorsAndSelf<PropertyDeclarationSyntax>() is { } declaration)
        {
            diagnostic.RegisterCodeFix("Make not-nullable.", context, (d, c) => ChangeDocument(d, declaration, c));
        }
    }

    private async Task<Document> ChangeDocument(DiagnosticContext context, PropertyDeclarationSyntax declaration, CancellationToken cancellation)
    {
        var oldNode = declaration.Type;
        var newName = Trim(oldNode is IdentifierNameSyntax alias
            ? await ResolveAlias(alias, context, cancellation)
            : oldNode.ToFullString().Trim());

        return context.Document.WithSyntaxRoot(context.Root.ReplaceNode(oldNode, IdentifierName(newName)));
    }

    private static async Task<string> ResolveAlias(IdentifierNameSyntax alias, DiagnosticContext context, CancellationToken cancellation) 
        => (await context.GetSemanticModelAsync(cancellation))
            .GetAliasInfo(alias, cancellation)!.Target.ToDisplayString();

    /// <remarks>
    /// This trim works both for Nullable&lt;T&gt;
    /// as for T?.
    /// </remarks>
    private static string Trim(string fullName)
    {
        var index = fullName.IndexOf('<');
        return fullName.Substring(index + 1, fullName.Length - index - 2);
    }
}
