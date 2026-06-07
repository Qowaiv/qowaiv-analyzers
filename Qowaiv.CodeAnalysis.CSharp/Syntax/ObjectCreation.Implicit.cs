namespace Qowaiv.CodeAnalysis.Syntax;

public partial class ObjectCreation
{
    internal sealed class Implicit(ImplicitObjectCreationExpressionSyntax node, SemanticModel semanticModel)
        : ObjectCreation(node, semanticModel)
    {
        private readonly ImplicitObjectCreationExpressionSyntax TypedNode = node;

        public override IReadOnlyList<Argument> Arguments
           => field ??= [.. TypedNode.ArgumentList?.Arguments.Select((a, i) => new Argument(this, a, i, SemanticModel)) ?? []];
    }
}
