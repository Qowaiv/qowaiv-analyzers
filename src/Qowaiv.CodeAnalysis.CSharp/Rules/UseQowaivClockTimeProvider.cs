namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UseQowaivClockTimeProvider() : CodingRule(Rule.UseQowaivClockTimeProvider)
{
    protected override void Register(AnalysisContext context)
    {
        context.RegisterSyntaxNodeAction(ReportArgumentList, SyntaxKind.ArgumentList);
        context.RegisterSyntaxNodeAction(ReportAssignment, SyntaxKind.SimpleAssignmentExpression);
    }

    private void ReportArgumentList(SyntaxNodeAnalysisContext context)
    {
        foreach (var argument in context.Node.Cast<ArgumentListSyntax>().Arguments)
        {
            if (argument?.Expression is { } expression)
            {
                Report(context, expression);
            }
        }
    }

    private void ReportAssignment(SyntaxNodeAnalysisContext context)
    {
        if (context.Node.Cast<AssignmentExpressionSyntax>().Right is { } node)
        {
            Report(context, node);
        }
    }

    private void Report(SyntaxNodeAnalysisContext context, ExpressionSyntax node)
    {
        if (context.SemanticModel.GetTypeInfo(node).Type is { } type
            && type.IsAssignableTo(SystemType.System.TimeProvider)
            && !IsQowaivClockTrimeProvider(context, node))
        {
            context.ReportDiagnostic(Diagnostic, node);
        }
    }

    private static bool IsQowaivClockTrimeProvider(SyntaxNodeAnalysisContext context, ExpressionSyntax node)
        => node is MemberAccessExpressionSyntax
        && context.SemanticModel.GetSymbolInfo(node).Symbol is IFieldSymbol field
        && field.ContainingType.Is(SystemType.Qowaiv.Clock)
        && field.Name == "TimeProvider";
}
