namespace Qowaiv.CodeAnalysis.Syntax;

public abstract partial class MemberDeclaration(SyntaxNode node, SemanticModel semanticModel)
    : SyntaxAbstraction<INamedTypeSymbol>(node, semanticModel)
{
    public abstract SyntaxList<AttributeListSyntax> AttributeLists { get; }

    public IEnumerable<AttributeDecoration> Attributes => AttributeLists
        .SelectMany(a => a.Attributes)
        .Select(a => new AttributeDecoration(a, SemanticModel));
}
