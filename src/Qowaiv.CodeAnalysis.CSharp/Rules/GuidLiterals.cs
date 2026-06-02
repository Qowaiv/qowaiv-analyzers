namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class GuidLiterals() : CodingRule(
    Rule.GuidLiteralsMustBeCompliant,
    Rule.GuidLiteralsShouldBeProvided,
    Rule.UseGuidParseOrEmpty)
{
    protected override void Register(AnalysisContext context)
    {
        RegisterSyntaxNodeAction(
            context,
            Ctor,
            SyntaxKind.ObjectCreationExpression,
            SyntaxKind.ImplicitObjectCreationExpression);

        RegisterSyntaxNodeAction(
           context,
           Method,
           SyntaxKind.InvocationExpression);
    }

    private static void Ctor(SyntaxNodeAnalysisContext context)
    {
        var node = context.Node.ObjectCreation(context.SemanticModel);

        if (node.Symbol?.ContainingType.Is(SystemType.System.Guid) is true)
        {
            if (node.Arguments.Count is 1 && node.Arguments[0].Expression is LiteralExpressionSyntax literal)
            {
                Check(literal, context);
            }
            else if (node.Arguments.Count is 0)
            {
                context.ReportDiagnostic(Rule.UseGuidParseOrEmpty, node);
            }
        }
    }

    private static void Method(SyntaxNodeAnalysisContext context)
    {
        var invocation = context.Node.Cast<InvocationExpressionSyntax>();
        var access = invocation.Expression as MemberAccessExpressionSyntax;

        if (access?.Name() == nameof(Guid.Parse))
        {
            var info = context.SemanticModel.GetSymbolInfo(access);

            if (info.Symbol?.ContainingType.Is(SystemType.System.Guid) is true
                && invocation.ArgumentList.Arguments[0].Expression is LiteralExpressionSyntax literal)
            {
                Check(literal, context);
            }

            if (info.CandidateSymbols.Any(t => t.ContainingType.Is(SystemType.System.Guid)))
            {
                var report = invocation.ArgumentList?.Arguments.FirstOrDefault()?.Expression ?? access;
                context.ReportDiagnostic(Rule.GuidLiteralsShouldBeProvided, report);
            }
        }
    }

    private static void Check(LiteralExpressionSyntax literal, SyntaxNodeAnalysisContext context)
    {
        var text = literal.Token.Text.Trim('"');

        if (!Guid.TryParse(text, out _))
        {
            context.ReportDiagnostic(Rule.GuidLiteralsMustBeCompliant, literal, text);
        }
    }
}
