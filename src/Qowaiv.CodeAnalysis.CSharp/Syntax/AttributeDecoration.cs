namespace Qowaiv.CodeAnalysis.Syntax;

public sealed class AttributeDecoration(AttributeSyntax node, SemanticModel semanticModel) : SyntaxAbstraction<INamedTypeSymbol>(node, semanticModel)
{
    private readonly AttributeSyntax TypedNode = node;

    /// <summary>
    /// Returns true if the name of the attribute matches, with or without the
    /// Attribute-suffix.
    /// </summary>
    public bool HasName(string name)
        => Name.Matches(name)
        || Name.Matches(name + "Attribute");

    protected override INamedTypeSymbol? GetSymbol(SemanticModel semanticModel)
        => semanticModel.GetSymbolInfo(TypedNode).Symbol is IMethodSymbol symbol
        ? symbol.ReceiverType as INamedTypeSymbol
        : null;
}
