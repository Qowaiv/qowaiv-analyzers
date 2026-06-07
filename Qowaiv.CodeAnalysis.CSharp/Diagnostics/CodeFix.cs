namespace Qowaiv.CodeAnalysis.Diagnostics;

/// <summary>Base implementation of a <see cref="CodeFixProvider"/> for the Qowaiv analyzers.</summary>
public abstract class CodeFix : CodeFixProvider
{
    protected CodeFix(string diagnosticId) => FixableDiagnosticIds = [diagnosticId];

    protected CodeFix(string diagnosticId, params string[] additional) => FixableDiagnosticIds = [diagnosticId, ..additional];

    /// <inheritdoc />
    public sealed override ImmutableArray<string> FixableDiagnosticIds { get; }

    /// <inheritdoc />
    public sealed override FixAllProvider? GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;
}
