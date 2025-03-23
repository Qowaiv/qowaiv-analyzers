using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Qowaiv.CodeAnalysis.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public sealed class UseQowaivClock() : CodeFix(Rule.UseTestableTimeProvider.Id, Rule.S6354)
{
    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        if (await context.ChangeDocumentContext() is { Node: { } } changeDoc
            && changeDoc.Node.AncestorsAndSelf<MemberAccessExpressionSyntax>() is { } member)
        {
            changeDoc.RegisterFix("Use Qowaiv.Clock.", context, c => ChangeDocument(member, c));
        }
    }

    private static async Task<Document> ChangeDocument(MemberAccessExpressionSyntax oldNode, ChangeDocumentContext context)
    {
        var model = await context.GetSemanticModelAsync();
        var property = (IPropertySymbol)model.GetSymbolInfo(oldNode, context.Cancellation).Symbol!;

        var clock = IdentifierName("Clock");
        var method = Method(property);

        var newNode = InvocationExpression(
            MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, clock, method),
            Arguments(property));

        return await context.ReplaceNode(oldNode, newNode);
    }

    private static IdentifierNameSyntax Method(IPropertySymbol property)
        => property.MemberOf(SystemType.System_DateTimeOffset)
            ? IdentifierName("NowWithOffset")
            : IdentifierName(property.Name);

    private static ArgumentListSyntax Arguments(IPropertySymbol property)
        => property.MemberOf(SystemType.System_DateTimeOffset) && property.Name == "UtcNow"
           ? ArgumentList(SeparatedList(new[] { Argument(TimeZoneInfo_Utc()) }))
           : ArgumentList(default);

    private static MemberAccessExpressionSyntax TimeZoneInfo_Utc() => MemberAccessExpression(
        SyntaxKind.SimpleMemberAccessExpression,
        IdentifierName(nameof(TimeZoneInfo)),
        IdentifierName(nameof(TimeZoneInfo.Utc)));
}
