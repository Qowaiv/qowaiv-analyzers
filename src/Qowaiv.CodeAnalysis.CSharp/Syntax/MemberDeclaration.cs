namespace Qowaiv.CodeAnalysis.Syntax;

public abstract partial class MemberDeclaration : SyntaxAbstraction<ISymbol>
{
    protected MemberDeclaration(SyntaxNode node, SemanticModel semanticModel)
        : base(node, semanticModel)
    {
    }

    public abstract SyntaxList<AttributeListSyntax> AttributeLists { get; }

    public IEnumerable<AttributeSyntax> Attributes => AttributeLists.SelectMany(a => a.Attributes);
}
