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
            && !IsFormatted(node.Token.Text);
    }

    [Pure]
    public static bool IsFormatted(ReadOnlySpan<char> token)
    {
        if (char.IsLetter(token[^1])) token = token[..^1];

        var first = true;
        var len = 0;
        var dot = false;

        foreach (var c in token)
        {
            if (c is '_' or '.')
            {
                if (!Valid()) return false;
                first = false;
                len = 0;
                dot |= c is '.';
            }
            else len++;
        }

        return Valid();

        bool Valid()
        {
            // no decimals or at least 2.
            if (dot) return len >= 2;
            // The first chain should be between 1 and 3.
            else if (first) return len is >= 1 and <= 3;
            // Other blocks should be exactly 3.
            return len is 3;
        }
    }
}
