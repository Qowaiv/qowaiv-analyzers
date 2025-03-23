namespace Qowaiv.CodeAnalysis.Syntax;

public partial class MemberDeclaration
{
    internal sealed class Parameter(ParameterSyntax node, SemanticModel semanticModel) : MemberDeclaration(node, semanticModel)
    {
        private readonly ParameterSyntax TypedNode = node;

        public override SyntaxList<AttributeListSyntax> AttributeLists => TypedNode.AttributeLists;

        [Pure]
        protected override ITypeSymbol? GetSymbol(SemanticModel semanticModel)
            => semanticModel.GetDeclaredSymbol(TypedNode) is IParameterSymbol symbol
                ? symbol.Type
                : null;
    }
}
