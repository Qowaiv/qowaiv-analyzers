namespace Qowaiv.CodeAnalysis.Diagnostics;

public abstract class CodeFix : CodeFixProvider
{
    protected CodeFix(string diagnosticId)
        => FixableDiagnosticIds = diagnosticId.Singleton().ToImmutableArray();

    protected CodeFix(string diagnosticId, params string[] additional)
       => FixableDiagnosticIds = diagnosticId.Singleton().Concat(additional).ToImmutableArray();

    public sealed override ImmutableArray<string> FixableDiagnosticIds { get; }

    public sealed override FixAllProvider? GetFixAllProvider() => null;
}
