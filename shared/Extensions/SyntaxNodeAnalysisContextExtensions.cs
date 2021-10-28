using System.Linq;

namespace Microsoft.CodeAnalysis.Diagnostics
{
    /// <summary>Extensions on <see cref="SyntaxNodeAnalysisContext"/>.</summary>
    public static class SyntaxNodeAnalysisContextExtensions
    {
        /// <summary>Report a <see cref="Diagnostic"/> about a  <see cref="SyntaxNode"/>'.</summary>
        public static void ReportDiagnostic(
            this SyntaxNodeAnalysisContext context,
            DiagnosticDescriptor descriptor,
            SyntaxNode node,
            params object[] messageArgs)
            => context.ReportDiagnostic(
                Diagnostic.Create(
                    descriptor,
                    node.GetLocation(),
                    messageArgs));
    }
}
