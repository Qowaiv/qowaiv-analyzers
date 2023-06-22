namespace Qowaiv.CodeAnalysis.Diagnostics;

public abstract class CodingRule : DiagnosticAnalyzer
{
    protected CodingRule(params DiagnosticDescriptor[] supportedDiagnostics)
        => SupportedDiagnostics = supportedDiagnostics.ToImmutableArray();

    public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }

    public DiagnosticDescriptor Diagnostic => SupportedDiagnostics[0];

    public sealed override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        Register(context);
    }

    protected abstract void Register(AnalysisContext context);
}
