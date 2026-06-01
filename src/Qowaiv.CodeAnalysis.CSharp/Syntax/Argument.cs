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

    /// <inheritdoc />
    protected override IParameterSymbol? GetSymbol(SemanticModel semanticModel)
        => TypedNode.NameColon?.Name() is { } name
        && TypedParent.LazySymbol.Value?.Parameters.FirstOrDefault(p => p.Name == name) is { } named
        ? named
        : TypedParent.LazySymbol.Value?.Parameters[Index];
}
