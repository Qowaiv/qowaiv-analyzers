using System.Reflection;

namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class ParseShouldNotFail : DiagnosticAnalyzer
{
    public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => Description.ParseShouldNotFail.Array();

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(Report, SyntaxKind.InvocationExpression);
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
            context.ReportDiagnostic(Description.ParseShouldNotFail, invocation, failure);
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
