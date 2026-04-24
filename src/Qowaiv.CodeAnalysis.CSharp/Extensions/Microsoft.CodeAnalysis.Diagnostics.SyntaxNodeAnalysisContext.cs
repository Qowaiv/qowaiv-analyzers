namespace Microsoft.CodeAnalysis.Diagnostics;

/// <summary>Extensions on <see cref="SyntaxNodeAnalysisContext"/>.</summary>
internal static class SyntaxNodeAnalysisContextExtensions
{
    extension(SyntaxNodeAnalysisContext context)
    {
        /// <summary>Report a <see cref="Diagnostic"/> about a <see cref="SyntaxNode"/>'.</summary>
        public void ReportDiagnostic(DiagnosticDescriptor descriptor, SyntaxNode node, params object[] messageArgs)
            => context.ReportDiagnostic(
                Diagnostic.Create(
                    descriptor,
                    node.GetLocation(),
                    messageArgs));

        /// <summary>Report a <see cref="Diagnostic"/> about a <see cref="SyntaxToken"/>'.</summary>
        public void ReportDiagnostic(DiagnosticDescriptor descriptor, SyntaxToken token, params object[] messageArgs)
            => context.ReportDiagnostic(
                Diagnostic.Create(
                    descriptor,
                    token.GetLocation(),
                    messageArgs));

        /// <summary>Tries to read the `dotnet_diagnostic.{descriptor.Id}.{property}` option.</summary>
        public string? TryGetConfiguredProperty(DiagnosticDescriptor descriptor,string property)
            => context.Options.AnalyzerConfigOptionsProvider
                .GetOptions(context.Node.SyntaxTree)
                .TryGetValue($"dotnet_diagnostic.{descriptor.Id}.{property}", out var value)
                ? value
                : null;
    }
}
