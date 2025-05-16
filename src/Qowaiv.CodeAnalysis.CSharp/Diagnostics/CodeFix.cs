namespace Qowaiv.CodeAnalysis.Diagnostics;

public abstract class CodeFix : CodeFixProvider
{
    protected CodeFix(string diagnosticId) => FixableDiagnosticIds = [diagnosticId];

    protected CodeFix(string diagnosticId, params string[] additional) => FixableDiagnosticIds = [diagnosticId, ..additional];

    public sealed override ImmutableArray<string> FixableDiagnosticIds { get; }

    public sealed override FixAllProvider? GetFixAllProvider() => null;
}
