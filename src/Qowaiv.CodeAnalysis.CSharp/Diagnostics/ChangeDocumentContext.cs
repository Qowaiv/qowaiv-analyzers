using Microsoft.CodeAnalysis.CodeActions;

namespace Qowaiv.CodeAnalysis.Diagnostics;

internal sealed class ChangeDocumentContext(
    Document document,
    Diagnostic diagnostic,
    SyntaxNode root,
    CancellationToken cancellation)
{
    public Document Document { get; } = document;

    public Diagnostic Diagnostic { get; } = diagnostic;

    public SyntaxNode Root { get; } = root;

    public CancellationToken Cancellation { get; } = cancellation;

    public async Task<SemanticModel> GetSemanticModelAsync()
    {
        SemanticModel ??= await Document.GetSemanticModelAsync(Cancellation);
        return SemanticModel!;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private SemanticModel? SemanticModel;

    public SyntaxToken Token => Root.FindToken(Diagnostic.Location.SourceSpan.Start);

    public SyntaxNode? Node => node ??= Root.FindNode(Diagnostic.Location.SourceSpan);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private SyntaxNode? node;

    public void RegisterFix(
            string title,
            CodeFixContext context,
            Func<ChangeDocumentContext, Task<Document>> createChangedDocument)

        => context.RegisterCodeFix(CodeAction.Create(title, _ => createChangedDocument(this)), Diagnostic);

    public Task<Document> ReplaceNode(SyntaxNode oldNode, SyntaxNode newNode)
        => Task.FromResult(Document.WithSyntaxRoot(Root.ReplaceNode(oldNode, newNode)));
}
