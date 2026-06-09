using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Qowaiv.CodeAnalysis.CodeFixes
{
    [ExportCodeFixProvider(LanguageNames.CSharp)]
    public sealed class GenerateUuid(Func<string> next) : CodeFix(
        Rule.UuidLiteralsMustBeCompliant.Id,
        Rule.UuidLiteralsShouldBeProvided.Id,
        Rule.UseUuidParseOrEmpty.Id)
    {
        public GenerateUuid() : this(UUID.Next) { }

        /// <summary>Injectable (for testing purposes) GUID generator.</summary>
        private readonly Func<string> Next = next;

        public override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            if ((await context.ChangeDocumentContext() is { } document) && Node(document.Node) is { } node)
            {
                document.RegisterFix("Generate UUID", context, _ => Change(node, document));
            }
        }

        private static SyntaxNode? Node(SyntaxNode? node)
            => (SyntaxNode?)node?.AncestorsAndSelf<InvocationExpressionSyntax>()
            ?? node?.AncestorsAndSelf<BaseObjectCreationExpressionSyntax>();

        private Task<Document> Change(SyntaxNode original, ChangeDocumentContext context)
            => context.ReplaceNode(original, InvocationExpression(UuidParse, Arg()));

        private static readonly MemberAccessExpressionSyntax UuidParse = MemberAccessExpression(
            SyntaxKind.SimpleMemberAccessExpression,
            IdentifierName("Uuid"),
            IdentifierName("Parse"));

        private ArgumentListSyntax Arg() => ArgumentList(
            SingletonSeparatedList(
                Argument(
                    LiteralExpression(
                        SyntaxKind.StringLiteralExpression,
                        Literal(Next())))));
    }
}
