namespace Qowaiv.CodeAnalysis.Diagnostics;

public abstract class CodingRule(DescriptorContainer supportedDiagnostic, params IEnumerable<DiagnosticDescriptor> additional)
    : DiagnosticAnalyzer
{
    public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [supportedDiagnostic, .. additional];

    public DiagnosticDescriptor Diagnostic => SupportedDiagnostics[0];

    /// <summary>If enabled, the coding also applies on test code (default false).</summary>
    protected bool AnalyzeTestCode { get; } = supportedDiagnostic.AnalyzeTestCode;

    /// <inheritdoc />
    public sealed override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        Register(context);
    }

    /// <summary>Register actions on the analysis context.</summary>
    protected abstract void Register(AnalysisContext context);

    /// <inheritdoc cref="AnalysisContext.RegisterSyntaxNodeAction{TLanguageKindEnum}(Action{SyntaxNodeAnalysisContext}, ImmutableArray{TLanguageKindEnum})"/>
    /// <remarks>
    /// When diagnose on test code is diabled, the action is made conditional too.
    /// </remarks>
    protected void RegisterSyntaxNodeAction(
        AnalysisContext context,
        Action<SyntaxNodeAnalysisContext> action,
        params SyntaxKind[] syntaxKinds)
        => context.RegisterSyntaxNodeAction(Action(action), syntaxKinds);

    private Action<SyntaxNodeAnalysisContext> Action(Action<SyntaxNodeAnalysisContext> action)
        => AnalyzeTestCode
        ? action
        : c =>
        {
            if (!c.IsTestCode)
            {
                action(c);
            }
        };
}
