using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UseFormattedAmountLiterals() : CodingRule(Rule.UseFormattedAmountLiterals)
{
    protected override void Register(AnalysisContext context)
        => RegisterSyntaxNodeAction(context, Report, SyntaxKind.NumericLiteralExpression);
    
    private void Report(SyntaxNodeAnalysisContext context)
    {
        var node = context.Node.Cast<LiteralExpressionSyntax>();

        if ((Cast() ?? UnformattedExpression()) is { } parent)
        {
            context.ReportDiagnostic(Diagnostic, parent);
        }

        SyntaxNode? Cast()
            => Trim(node.Parent) is CastExpressionSyntax cast
            && cast.Type.TypeNode(context.SemanticModel).Symbol.Is(SystemType.Qowaiv.Financial.Amount)
            ? cast
            : null;

        SyntaxNode? UnformattedExpression()
            => Trim(node.Parent) is MemberAccessExpressionSyntax expression
            && expression.Method(context.SemanticModel) is { Name: "Amount", Parameters.Length: 0 } method
            && method.ReturnType.Is(SystemType.Qowaiv.Financial.Amount)
            && !Amount.IsFormatted(node.Token.Text)
            ? expression.Parent
            : null;
    }

    private static SyntaxNode? Trim(SyntaxNode? node) => node switch
    {
        null => null,
        ParenthesizedExpressionSyntax or 
        PrefixUnaryExpressionSyntax => Trim(node.Parent),
        _ => node,
    };
}
