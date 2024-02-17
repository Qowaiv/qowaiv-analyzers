namespace Qowaiv.CodeAnalysis.Syntax;

public partial class TypeDeclaration
{
    internal sealed class Struct(StructDeclarationSyntax node, SemanticModel model) : TypeDeclaration(node, model)
    {
        private readonly StructDeclarationSyntax TypedNode = node;

        public override SyntaxList<AttributeListSyntax> AttributeLists => TypedNode.AttributeLists;

        public override IEnumerable<SyntaxKind> Modifiers => TypedNode.Modifiers.Select(m => m.Kind());
    }
}
