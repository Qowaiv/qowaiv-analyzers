namespace Qowaiv.CodeAnalysis.Syntax;

public sealed class AttributeDecoration(AttributeSyntax node, SemanticModel semanticModel) : SyntaxAbstraction<ITypeSymbol>(node, semanticModel)
{
    private readonly AttributeSyntax TypedNode = node;

    protected override ITypeSymbol? GetSymbol(SemanticModel semanticModel)
        => semanticModel.GetSymbolInfo(TypedNode).Symbol is IMethodSymbol symbol
        ? symbol.ReceiverType as INamedTypeSymbol
        : null;
}
