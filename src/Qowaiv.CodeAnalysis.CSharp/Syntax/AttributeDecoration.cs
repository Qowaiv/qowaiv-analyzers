namespace Qowaiv.CodeAnalysis.Syntax;

public sealed class AttributeDecoration(AttributeSyntax node, SemanticModel semanticModel) : SyntaxAbstraction<INamedTypeSymbol>(node, semanticModel)
{
    private readonly AttributeSyntax TypedNode = node;

    protected override INamedTypeSymbol? GetSymbol(SemanticModel semanticModel)
        => semanticModel.GetSymbolInfo(TypedNode).Symbol is IMethodSymbol symbol
        ? symbol.ReceiverType as INamedTypeSymbol
        : null;
}
