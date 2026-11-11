namespace Qowaiv.CodeAnalysis.Syntax;

public sealed class MethodDeclaration(BaseMethodDeclarationSyntax methodNode, SemanticModel semanticModel) 
    : SyntaxAbstraction<IMethodSymbol>(methodNode, semanticModel)
{
    public BaseMethodDeclarationSyntax TypedNode { get; } = methodNode;

    public IEnumerable<SyntaxKind> Modifiers => TypedNode.Modifiers.Select(m => m.Kind());

    public IEnumerable<AttributeDecoration> Attributes => TypedNode.AttributeLists
       .SelectMany(a => a.Attributes)
       .Select(a => new AttributeDecoration(a, SemanticModel));

    public bool IsPartial => Modifiers.Contains(SyntaxKind.PartialKeyword);

    /// <summary>Returns true if the method is declared with the <c>void</c> return type.</summary>
    public bool IsVoid => TypedNode is MethodDeclarationSyntax method
        && method.ReturnType is PredefinedTypeSyntax predefined
        && predefined.Keyword.IsKind(SyntaxKind.VoidKeyword);

    /// <summary>Returns true if the method is decorated with the <c>readonly</c>> modifier.</summary>
    public bool IsReadOnly => Modifiers.Contains(SyntaxKind.ReadOnlyKeyword);

    public bool IsObsolete => Attributes.Any(a => a.IsObsoleteAttribute)
            || (IsPartial && Symbol is { IsObsolete: true });

    [Pure]
    protected override IMethodSymbol? GetSymbol(SemanticModel semanticModel)
        => semanticModel.GetDeclaredSymbol(TypedNode);
}
