namespace Qowaiv.CodeAnalysis.Syntax;

public partial class TypeNode
{
    internal sealed class Array(ArrayTypeSyntax arrayNode, SemanticModel semanticModel) : TypeNode(arrayNode, semanticModel)
    {
        public override bool IsArray => true;

        public override TypeNode ElementType { get; } = arrayNode.ElementType.TypeNode(semanticModel);
    }
}
