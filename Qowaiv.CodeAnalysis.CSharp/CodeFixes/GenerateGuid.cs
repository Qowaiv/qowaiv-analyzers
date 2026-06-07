using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Qowaiv.CodeAnalysis.CodeFixes
{
    [ExportCodeFixProvider(LanguageNames.CSharp)]
    public sealed class GenerateGuid(Func<Guid> next) : CodeFix(
        Rule.GuidLiteralsMustBeCompliant.Id,
        Rule.GuidLiteralsShouldBeProvided.Id,
        Rule.UseGuidParseOrEmpty.Id)
    {
        public GenerateGuid() : this(Guid.NewGuid) { }

        /// <summary>Injectable (for testing purposes) GUID generator.</summary>
        private readonly Func<Guid> Next = next;

        public override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            if ((await context.ChangeDocumentContext() is { } document) && Node(document.Node) is { } node)
            {
                document.RegisterFix("Generate GUID", context, _ => Change(node, document));
            }
        }

        private static SyntaxNode? Node(SyntaxNode? node)
            => (SyntaxNode?)node?.AncestorsAndSelf<InvocationExpressionSyntax>()
            ?? node?.AncestorsAndSelf<BaseObjectCreationExpressionSyntax>();

        private Task<Document> Change(SyntaxNode original, ChangeDocumentContext context)
            => context.ReplaceNode(original, InvocationExpression(GuidParse, Arg()));

        private static readonly MemberAccessExpressionSyntax GuidParse = MemberAccessExpression(
            SyntaxKind.SimpleMemberAccessExpression,
            IdentifierName(nameof(Guid)),
            IdentifierName(nameof(Guid.Parse)));

        private ArgumentListSyntax Arg() => ArgumentList(
            SingletonSeparatedList(
                Argument(
                    LiteralExpression(
                        SyntaxKind.StringLiteralExpression,
                        Literal(Next().ToString())))));
    }
}
