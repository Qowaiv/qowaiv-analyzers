namespace Qowaiv.CodeAnalysis.Syntax;

public partial class TypeDeclaration
{
    internal sealed class Interface(InterfaceDeclarationSyntax node, SemanticModel model) : TypeDeclaration(node, model)
    {
        private readonly InterfaceDeclarationSyntax TypedNode = node;

        public override SyntaxList<AttributeListSyntax> AttributeLists => TypedNode.AttributeLists;

        public override IEnumerable<SyntaxKind> Modifiers => TypedNode.Modifiers.Select(m => m.Kind());
    }
}
