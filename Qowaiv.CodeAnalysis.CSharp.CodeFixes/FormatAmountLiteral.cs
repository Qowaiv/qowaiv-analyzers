using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Qowaiv.CodeAnalysis.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public sealed class FormatAmountLiteral() : CodeFix(Rule.FormatAmountLiterals.Id)
{
    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        if (await context.ChangeDocumentContext() is { Node: { } expression } change)
        {
            change.RegisterFix("Format amount", context, d => Change(expression, d));
        }
    }

    [Pure]
    private static Task<Document> Change(SyntaxNode parent, ChangeDocumentContext context)
    {
        var replacement = Resolve(parent, false) is { } resolved
            ? Member(resolved.Expression, resolved.Negate)
            : parent;

        return context.ReplaceNode(parent, replacement);
    }

    [Pure]
    private static (LiteralExpressionSyntax Expression, bool Negate)? Resolve(SyntaxNode? node, bool negate) => node switch
    {
        LiteralExpressionSyntax n => (n, negate),
        CastExpressionSyntax n => Resolve(n.Expression, negate),
        InvocationExpressionSyntax n => Resolve(n.Expression, negate),
        MemberAccessExpressionSyntax n => Resolve(n.Expression, negate),
        ParenthesizedExpressionSyntax n => Resolve(n.Expression, negate),
        PrefixUnaryExpressionSyntax n => Resolve(n.Operand, !negate),
        _ => null,
    };

    [Pure]
    private static InvocationExpressionSyntax Member(LiteralExpressionSyntax literal, bool negate)
       => InvocationExpression(
           MemberAccessExpression(
               SyntaxKind.SimpleMemberAccessExpression,
               Negate(Format(literal), negate),
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
