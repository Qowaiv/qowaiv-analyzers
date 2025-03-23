namespace Microsoft.CodeAnalysis.Diagnostics;

/// <summary>Extensions on <see cref="SyntaxNodeAnalysisContext"/>.</summary>
internal static class SyntaxNodeAnalysisContextExtensions
{
    /// <summary>Report a <see cref="Diagnostic"/> about a <see cref="SyntaxNode"/>'.</summary>
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

    /// <summary>Report a <see cref="Diagnostic"/> about a <see cref="SyntaxToken"/>'.</summary>
    public static void ReportDiagnostic(
        this SyntaxNodeAnalysisContext context,
        DiagnosticDescriptor descriptor,
        SyntaxToken token,
        params object[] messageArgs)
        => context.ReportDiagnostic(
            Diagnostic.Create(
                descriptor,
                token.GetLocation(),
                messageArgs));

    /// <summary>Tries to read the `dotnet_diagnostic.{descriptor.Id}.{property}` option.</summary>
    public static string? TryGetConfiguredProperty(
        this SyntaxNodeAnalysisContext context,
        DiagnosticDescriptor descriptor,
        string property)
        => context.Options.AnalyzerConfigOptionsProvider
            .GetOptions(context.Node.SyntaxTree)
            .TryGetValue($"dotnet_diagnostic.{descriptor.Id}.{property}", out var value)
            ? value
            : null;
}
