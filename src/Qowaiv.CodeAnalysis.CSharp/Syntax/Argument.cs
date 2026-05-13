using Microsoft.CodeAnalysis.Operations;

namespace Qowaiv.CodeAnalysis.Syntax;

public sealed class Argument(SyntaxAbstraction<IMethodSymbol> parent, ArgumentSyntax node, int index, SemanticModel semanticModel)
    : SyntaxAbstraction<IParameterSymbol>(node, semanticModel)
{
    public SyntaxAbstraction<IMethodSymbol> TypedParent { get; } = parent;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly ArgumentSyntax TypedNode = node;

    public int Index { get; } = index;

    /// <inheritdoc cref="ArgumentSyntax.Expression" />
    public ExpressionSyntax Expression => TypedNode.Expression;

    /// <summary>The argument is a <see cref="LiteralExpressionSyntax"/>.</summary>
    public bool IsLiteral => Expression is LiteralExpressionSyntax;

    protected override IParameterSymbol? GetSymbol(SemanticModel semanticModel)
        => TypedParent.LazySymbol.Value?.Parameters[Index];
}
