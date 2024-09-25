
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
        if (context.Node is BinaryExpressionSyntax binary
            && Nullable(binary.Left,binary.Right) is { } nullBinary)
        {
            context.ReportDiagnostic(Diagnostic, nullBinary);
        }
        else if (context.Node is AssignmentExpressionSyntax assign
             && Nullable(assign.Left, assign.Right) is { } nullAssign)
        {
            context.ReportDiagnostic(Diagnostic, nullAssign);
        }

        SyntaxNode? Nullable(ExpressionSyntax left, ExpressionSyntax right)
        {
            var l = IsNullable(left);
            var r = IsNullable(right);

            return context switch
            {
                _ when l && r => context.Node,
                _ when l => left,
                _ when r => right,
                _ => null,
            };
        }

        bool IsNullable(ExpressionSyntax expression)
            => context.SemanticModel.GetTypeInfo(expression).Type?.IsNullableValueType() is true;
    }
}
