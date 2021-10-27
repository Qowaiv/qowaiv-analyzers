using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Qowaiv.CodeAnalysis.Diagnostics;
using Qowaiv.CodeAnalysis.Syntax;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace Qowaiv.CodeAnalysis
{
    public partial class ParseShouldNotFail : DiagnosticAnalyzer
    {
        public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => Description.ParseShouldNotFail;

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(c =>
            {
                var invocation = c.Node.InvocationExpression();
                if (invocation.Name() == nameof(Parse)
                    && invocation.Arguments.Count() == 1
                    && c.SemanticModel.GetConstantValue(invocation.Expressions.First()).Value is string literal
                    && c.SemanticModel.GetSymbolInfo(invocation).Symbol is IMethodSymbol method
                    && method.IsStatic
                    && SymbolEqualityComparer.Default.Equals(method.ContainingType, method.ReturnType)
                    && Parse(method, literal) is { } failure)
                {
                    c.ReportDiagnostic(this, invocation, failure);
                }
            },
            SyntaxKinds.InvocationExpression);
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
}
