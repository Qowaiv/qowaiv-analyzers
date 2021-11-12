using System.Reflection;

namespace Qowaiv.CodeAnalysis;

public partial class ParseShouldNotFail : DiagnosticAnalyzer
{
    private static readonly DiagnosticDescriptor Rule = Description.ParseShouldNotFail;
    public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => Rule.Array();

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(Report, SyntaxKinds.InvocationExpression);
    }

    private void Report(SyntaxNodeAnalysisContext context)
    {
        var invocation = context.Node.InvocationExpression();
        if (invocation.Name() == nameof(Parse)
            && invocation.Arguments.Count() == 1
            && context.SemanticModel.GetConstantValue(invocation.Expressions.First()).Value is string literal
            && context.SemanticModel.GetSymbolInfo(invocation).Symbol is IMethodSymbol method
            && method.IsStatic
            && SymbolEqualityComparer.Default.Equals(method.ContainingType, method.ReturnType)
            && Parse(method, literal) is { } failure)
        {
            context.ReportDiagnostic(Rule, invocation, failure);
        }
    }

    private static string Parse(IMethodSymbol symbol, string value)
    {
        if (symbol.GetMethodInfo() is { } method)
        {
            try
            {
                method.Invoke(null, new object[] { value });
            }
            catch (TargetInvocationException x)
            {
                return x.InnerException.Message ?? "Parsing failed.";
            }
        }
        return null;
    }
}
