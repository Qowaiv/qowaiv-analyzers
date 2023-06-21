using Microsoft.CodeAnalysis.CodeActions;

namespace Qowaiv.CodeAnalysis.Syntax;

internal sealed class DiagnosticContext
{
    public DiagnosticContext(Document document, Diagnostic diagnostic, SyntaxNode root)
    {
        Document = document;
        Diagnostic = diagnostic;
        Root = root;
    }

    public Document Document { get; }

    public Diagnostic Diagnostic { get; }
    
    public SyntaxNode Root { get; }

    public async Task<SemanticModel> GetSemanticModelAsync(CancellationToken cancellation = default)
    {
        SemanticModel ??= await Document.GetSemanticModelAsync(cancellation);
        return SemanticModel!;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private SemanticModel? SemanticModel;

    public SyntaxToken Token => Root.FindToken(Diagnostic.Location.SourceSpan.Start);

    public void RegisterCodeFix(
            string title,
            CodeFixContext context,
            Func<DiagnosticContext, CancellationToken, Task<Document>> createChangedDocument)

      => context.RegisterCodeFix(CodeAction.Create(title, c => createChangedDocument(this, c)), Diagnostic);
}
