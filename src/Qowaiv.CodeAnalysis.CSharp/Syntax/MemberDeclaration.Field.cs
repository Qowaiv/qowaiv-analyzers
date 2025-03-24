namespace Qowaiv.CodeAnalysis.Syntax;

public partial class MemberDeclaration
{
    internal sealed class Field(FieldDeclarationSyntax node, SemanticModel semanticModel) : MemberDeclaration(node, semanticModel)
    {
        private readonly FieldDeclarationSyntax TypedNode = node;

        public override SyntaxList<AttributeListSyntax> AttributeLists => TypedNode.AttributeLists;

        [Pure]
        protected override INamedTypeSymbol? GetSymbol(SemanticModel semanticModel)
            => semanticModel.GetTypeInfo(TypedNode.Declaration.Type).Type as INamedTypeSymbol;
    }
}
