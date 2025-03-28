namespace Qowaiv.CodeAnalysis.Syntax;

public partial class MemberDeclaration
{
    internal sealed class Property(PropertyDeclarationSyntax node, SemanticModel semanticModel) : MemberDeclaration(node, semanticModel)
    {
        private readonly PropertyDeclarationSyntax TypedNode = node;

        public override SyntaxList<AttributeListSyntax> AttributeLists => TypedNode.AttributeLists;

        [Pure]
        protected override INamedTypeSymbol? GetSymbol(SemanticModel semanticModel)
            => semanticModel.GetDeclaredSymbol(TypedNode) is IPropertySymbol property
                ? property.Type as INamedTypeSymbol
                : null;
    }
}
