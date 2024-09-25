namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class ApplyArithmeticOperationsOnNonNullablesOnly() : CodingRule(Rule.ApplyArithmeticOperationsOnNonNullablesOnly)
{
    protected override void Register(AnalysisContext context)
    {
        context.RegisterSyntaxNodeAction(
            Report,
            SyntaxKind.MultiplyExpression,
            SyntaxKind.DivideExpression,
            SyntaxKind.AddExpression,
            SyntaxKind.SubtractExpression,
            SyntaxKind.ModuloExpression,
            // Assignments
            SyntaxKind.MultiplyAssignmentExpression,
            SyntaxKind.DivideAssignmentExpression,
            SyntaxKind.AddAssignmentExpression,
            SyntaxKind.SubtractAssignmentExpression,
            SyntaxKind.ModuloAssignmentExpression);
    }

    private void Report(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is BinaryExpressionSyntax binary)
        {
            Report(context, binary.Left, binary.Right);
        }
        else if (context.Node is AssignmentExpressionSyntax assign)
        {
            Report(context, assign.Left, assign.Right);
        }
    }

    private void Report(SyntaxNodeAnalysisContext context, ExpressionSyntax left, ExpressionSyntax right)
    {
        var l = IsNullable(left);
        var r = IsNullable(right);

        if (l && r)
        {
            context.ReportDiagnostic(Diagnostic, context.Node, "Result of operation");
        }
        else if (l || r)
        {
            context.ReportDiagnostic(Diagnostic, l ? left : right, "Value of operand");
        }

        bool IsNullable(ExpressionSyntax expression)
            => context.SemanticModel.GetTypeInfo(expression).Type?.IsNullableValueType() is true;
    }
}
