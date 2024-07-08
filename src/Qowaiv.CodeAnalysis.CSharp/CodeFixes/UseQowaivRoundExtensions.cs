using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Qowaiv.CodeAnalysis.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public sealed class UseQowaivRoundExtensions() : CodeFix(Rule.UseQowaivDecimalRounding.Id)
{
    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        if (await context.ChangeDocumentContext() is { Node: InvocationExpressionSyntax syntax } change)
        {
            change.RegisterFix("Use Qowaiv .Round().", context, c => ApplySuggestion(syntax, c));
        }
    }

    private static Task<Document> ApplySuggestion(InvocationExpressionSyntax syntax, ChangeDocumentContext context)
    {
        var list = syntax.ArgumentList.Arguments;
        var dec = list[0].Expression;
        var name = syntax.Expression.Name();
        var args = Arguments(list.RemoveAt(0), name);

        var member = MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, dec, Round);
        var updated = InvocationExpression(member, ArgumentList(SeparatedList(args)));

        return context.ReplaceNode(syntax, updated);
    }

    private static IReadOnlyList<ArgumentSyntax> Arguments(SeparatedSyntaxList<ArgumentSyntax> args, string? method) => method switch
    {
        nameof(Math.Floor) or
        nameof(Math.Ceiling) or
        nameof(Math.Truncate) => [Zero, DecimalRounding(method)],
        _ when args.Any() && DecimalRounding(args.Last()) is { } last => WithZero(args, last),
        _ => args,
    };

    private static IReadOnlyList<ArgumentSyntax> WithZero(SeparatedSyntaxList<ArgumentSyntax> args, ArgumentSyntax last)
        => args.Count == 1
        ? [Zero, last]
        : [args[0], last];

    private static IdentifierNameSyntax Round => IdentifierName(nameof(Round));

    private static ArgumentSyntax Zero => Argument(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)));

    private static ArgumentSyntax DecimalRounding(string kind) => Argument(IdentifierName($"{nameof(DecimalRounding)}.{kind}"));

    private static ArgumentSyntax? DecimalRounding(ArgumentSyntax rounding)
        => rounding is { Expression: MemberAccessExpressionSyntax { Expression: IdentifierNameSyntax expression, Name: IdentifierNameSyntax name } }
        && expression.ToFullString().EndsWith(nameof(MidpointRounding))
        && Kinds.TryGetValue(name.ToFullString(), out var kind)
        ? DecimalRounding(kind)
        : null;

    private static readonly IReadOnlyDictionary<string, string> Kinds = new Dictionary<string, string>
    {
        ["ToEven"] = "ToEven",
        ["ToZero"] = "DirectTowardsZero",
        ["AwayFromZero"] = "AwayFromZero",
        ["ToPositiveInfinity"] = "Ceiling",
        ["ToNegativeInfinity"] = "Floor",
    };
}
