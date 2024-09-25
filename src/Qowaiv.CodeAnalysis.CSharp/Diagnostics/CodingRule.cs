namespace Qowaiv.CodeAnalysis.Diagnostics;

public abstract class CodingRule : DiagnosticAnalyzer
{
    protected CodingRule(DiagnosticDescriptor supportedDiagnostic, params DiagnosticDescriptor[] additional)
        => SupportedDiagnostics = [supportedDiagnostic, ..additional];

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
