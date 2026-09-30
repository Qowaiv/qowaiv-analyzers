using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class FormatAmountLiterals() : CodingRule(Rule.FormatAmountLiterals)
{
    protected override void Register(AnalysisContext context)
        => RegisterSyntaxNodeAction(context, Report, SyntaxKind.NumericLiteralExpression);
    
    private void Report(SyntaxNodeAnalysisContext context)
    {
        var node = context.Node.Cast<LiteralExpressionSyntax>();

        if (Cast() || UnformattedExpression())
        {
            context.ReportDiagnostic(Diagnostic, node.Parent!);
        }

        bool Cast()
            => node.Parent is CastExpressionSyntax cast
            && cast.Type.TypeNode(context.SemanticModel).Symbol.Is(SystemType.Qowaiv.Financial.Amount);

        bool UnformattedExpression()
            => node.Parent is MemberAccessExpressionSyntax expression
            && expression.Method(context.SemanticModel) is { Name: "Amount", Parameters.Length: 0 } method
            && method.ReturnType.Is(SystemType.Qowaiv.Financial.Amount)
            && !Amount.IsFormatted(node.Token.Text);
    }
}
