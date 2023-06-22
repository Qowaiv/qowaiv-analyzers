using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Qowaiv.CodeAnalysis.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public sealed class UseQowaivClock : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds => new[]
    {
        Rule.UseTestableTimeProvider.Id,
        "S6354",
    }
    .ToImmutableArray();

    public override FixAllProvider? GetFixAllProvider() => null;

    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        if (await context.DiagnosticContext() is { } diagnostic
            && diagnostic.Token.Parent!.AncestorsAndSelf<MemberAccessExpressionSyntax>() is { } member)
        {
            diagnostic.RegisterCodeFix("Use Qowaiv.Clock.", context, (d, c) => ChangeDocument(d, member, c));
        }
    }

    private static async Task<Document> ChangeDocument(DiagnosticContext context, MemberAccessExpressionSyntax oldNode, CancellationToken cancellation)
    {
        var model = await context.GetSemanticModelAsync(cancellation);
        var property = (IPropertySymbol)model.GetSymbolInfo(oldNode, cancellation).Symbol!;

        var clock = IdentifierName("Clock");
        var method = Method(property);

        var newNode = InvocationExpression(
            MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, clock, method),
            Arguments(property));

        return context.Document.WithSyntaxRoot(context.Root.ReplaceNode(oldNode, newNode));
    }

    private static IdentifierNameSyntax Method(IPropertySymbol property)
        => property.MemberOf(SystemType.System_DateTimeOffset)
        ? IdentifierName("NowWithOffset")
        : IdentifierName(property.Name);

    private static ArgumentListSyntax Arguments(IPropertySymbol property)
        => property.MemberOf(SystemType.System_DateTimeOffset) && property.Name == "UtcNow"
        ? ArgumentList(SeparatedList(new SyntaxNode[] { TimeZoneInfo_Utc() }))
        : ArgumentList(default);

    private static MemberAccessExpressionSyntax TimeZoneInfo_Utc() => MemberAccessExpression(
        SyntaxKind.SimpleMemberAccessExpression,
        IdentifierName(nameof(TimeZoneInfo)),
        IdentifierName(nameof(TimeZoneInfo.Utc)));
}
