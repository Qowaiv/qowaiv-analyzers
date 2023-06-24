using Microsoft.CodeAnalysis.CodeActions;

namespace Qowaiv.CodeAnalysis.CodeFixes;

internal sealed class ChangeDocumentContext
{
    public ChangeDocumentContext(Document document, Diagnostic diagnostic, SyntaxNode root, CancellationToken cancellation)
    {
        Document = document;
        Diagnostic = diagnostic;
        Root = root;
        Cancellation = cancellation;
    }

    public Document Document { get; }

    public Diagnostic Diagnostic { get; }

    public SyntaxNode Root { get; }

    public CancellationToken Cancellation { get; }

    public async Task<SemanticModel> GetSemanticModelAsync()
    {
        SemanticModel ??= await Document.GetSemanticModelAsync(Cancellation);
        return SemanticModel!;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private SemanticModel? SemanticModel;

    public SyntaxToken Token => Root.FindToken(Diagnostic.Location.SourceSpan.Start);

    public SyntaxNode? Node => Root.FindNode(Diagnostic.Location.SourceSpan);

    public void RegisterCodeFix(
            string title,
            CodeFixContext context,
            Func<ChangeDocumentContext, Task<Document>> createChangedDocument)

        => context.RegisterCodeFix(CodeAction.Create(title, _ => createChangedDocument(this)), Diagnostic);

    public Task<Document> ReplaceNode(SyntaxNode oldNode, SyntaxNode newNode)
        => Task.FromResult(Document.WithSyntaxRoot(Root.ReplaceNode(oldNode, newNode)));
}
