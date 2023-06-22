using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Qowaiv.CodeAnalysis.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public sealed class ChangePropertyTypeToNotNullable : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds => new[]
    {
        Rule.DefinePropertiesAsNotNullable.Id,
    }
    .ToImmutableArray();

    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var parent = (await context.DiagnosticContext()).Token.Parent;
        if (await context.DiagnosticContext() is { } diagnostic
            && GetTypeSyntax(diagnostic.Token.Parent) is { } type)
        {
            diagnostic.RegisterCodeFix("Change property type to not-nullable.", context, (d, c) => ChangeDocument(d, type, c));
        }
    }

    private static TypeSyntax? GetTypeSyntax(SyntaxNode? node)
        => node switch
        {
            null => null,
            PropertyDeclarationSyntax property => property.Type,
            ParameterSyntax parameter => parameter.Type,
            _ => GetTypeSyntax(node.Parent),
        };

    private static async Task<Document> ChangeDocument(DiagnosticContext context, TypeSyntax type, CancellationToken cancellation)
    {
        var newName = Trim(type is IdentifierNameSyntax alias
            ? await ResolveAlias(alias, context, cancellation)
            : type.ToFullString().Trim());

        return context.Document.WithSyntaxRoot(context.Root.ReplaceNode(type, IdentifierName(newName)));
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
