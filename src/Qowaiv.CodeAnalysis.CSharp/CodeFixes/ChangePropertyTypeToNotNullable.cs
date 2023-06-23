using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Qowaiv.CodeAnalysis.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public sealed class ChangePropertyTypeToNotNullable : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds => new[]
    {
        Rule.DefinePropertiesAsNotNullable.Id,
        Rule.DefineEnumPropertiesAsNotNullable.Id,
    }
    .ToImmutableArray();

    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        if (await context.DiagnosticContext() is { } diagnostic
            && GetTypeSyntax(diagnostic.Node) is { } type)
        {
            diagnostic.RegisterCodeFix("Change property type to not-nullable.", context, (d, c) => ChangeDocument(d, type, c));
        }
    }

    private static TypeSyntax? GetTypeSyntax(SyntaxNode? node)
        => node switch
        {
            null => null,
            NullableTypeSyntax nullable => nullable,
            PropertyDeclarationSyntax property => property.Type,
            ParameterSyntax parameter => parameter.Type,
            _ => GetTypeSyntax(node.Parent),
        };

    private static async Task<Document> ChangeDocument(DiagnosticContext context, TypeSyntax type, CancellationToken cancellation)
    {
        var fullName = type is IdentifierNameSyntax alias
            ? await ResolveAlias(alias, context, cancellation)
            : type.ToFullString();

        return context.Document.WithSyntaxRoot(context.Root.ReplaceNode(type, NewName(fullName.Trim())));
    }

    private static async Task<string> ResolveAlias(IdentifierNameSyntax alias, DiagnosticContext context, CancellationToken cancellation)
        => (await context.GetSemanticModelAsync(cancellation))
            .GetAliasInfo(alias, cancellation)!.Target.ToDisplayString();

    /// <remarks>
    /// This trim works both for Nullable&lt;T&gt;
    /// as for T?.
    /// </remarks>
    private static IdentifierNameSyntax NewName(string fullName)
    {
        var index = fullName.IndexOf('<');
        return IdentifierName(fullName.Substring(index + 1, fullName.Length - index - 2));
    }
}
