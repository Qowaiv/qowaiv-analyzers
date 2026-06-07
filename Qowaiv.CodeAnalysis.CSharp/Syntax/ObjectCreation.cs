namespace Qowaiv.CodeAnalysis.Syntax
{
    public abstract partial class ObjectCreation(ExpressionSyntax node, SemanticModel semanticModel)
        : SyntaxAbstraction<IMethodSymbol>(node, semanticModel)
    {
        public abstract IReadOnlyList<Argument> Arguments { get; }

        protected sealed override IMethodSymbol? GetSymbol(SemanticModel semanticModel)
            => semanticModel.GetSymbolInfo(Node).Symbol as IMethodSymbol;
    }
}
