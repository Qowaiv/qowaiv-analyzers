using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Qowaiv.CodeAnalysis.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public sealed class FormatAmount() : CodeFix(Rule.UseFormattedAmountLiterals.Id)
{
    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        if (await context.ChangeDocumentContext() is { Node: { } parent } change)
        {
            change.RegisterFix(
                "Format amount",
                context,
                // It is unclear where the argument syntax is comming from, but this works.
                d => Change(parent is ArgumentSyntax arg ? arg.Expression : parent, d));
        }
    }

    [Pure]
    private static Task<Document> Change(SyntaxNode parent, ChangeDocumentContext context) 
        => context.ReplaceNode(parent, Resolve(parent, false)?.WithTriviaFrom(parent) ?? parent);

    [Pure]
    private static InvocationExpressionSyntax? Resolve(SyntaxNode? node, bool negate) => node switch
    {
        LiteralExpressionSyntax n => Member(Negate(Format(n), negate)),
        CastExpressionSyntax n => Resolve(n.Expression, negate),
        InvocationExpressionSyntax n => Resolve(n.Expression, negate),
        MemberAccessExpressionSyntax n => Resolve(n.Expression, negate),
        ParenthesizedExpressionSyntax n => Resolve(n.Expression, negate),
        PrefixUnaryExpressionSyntax n => Resolve(n.Operand, !negate),
        _ => null,
    };

    [Pure]
    private static InvocationExpressionSyntax Member(ExpressionSyntax expression)
       => InvocationExpression(
           MemberAccessExpression(
               SyntaxKind.SimpleMemberAccessExpression,
               expression,
               IdentifierName("Amount")));

    [Pure]
    private static ExpressionSyntax Negate(LiteralExpressionSyntax literal, bool negate)
        => negate
        ? ParenthesizedExpression(PrefixUnaryExpression(SyntaxKind.UnaryMinusExpression, literal))
        : literal;

    [Pure]
    private static LiteralExpressionSyntax Format(LiteralExpressionSyntax literal)
        => LiteralExpression(SyntaxKind.NumericLiteralExpression, ParseToken(Amount.Format(literal.Token.Text)));
}
