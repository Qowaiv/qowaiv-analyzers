
namespace Qowaiv.CodeAnalysis.Syntax;

public partial class TypeNode
{
    internal sealed class Generic(GenericNameSyntax genericNode, SemanticModel semanticModel) : TypeNode(genericNode, semanticModel) 
    {
        public override bool IsGenericType => true;

        public override IReadOnlyList<TypeNode> GenericTypeArguments { get; }
            = genericNode.TypeArgumentList?.Arguments.Select(arg => arg.TypeNode(semanticModel)).ToArray() ?? [];
    }
}
