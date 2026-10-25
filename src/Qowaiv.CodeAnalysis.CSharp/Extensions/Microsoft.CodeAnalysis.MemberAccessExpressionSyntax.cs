#pragma warning disable S1200 // Classes should not be coupled to too many other classes
namespace Microsoft.CodeAnalysis;

public static class MemberAccessExpressionSyntaxExtensions
{
    extension(MemberAccessExpressionSyntax node)
    {
        [Pure]
        public IMethodSymbol? Method(SemanticModel model) 
            => model.GetSymbolInfo(node).Symbol as IMethodSymbol;
    }
}
