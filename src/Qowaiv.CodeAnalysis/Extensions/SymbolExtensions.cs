using Qowaiv.CodeAnalysis;

namespace Microsoft.CodeAnalysis
{
    public static class SymbolExtensions
    {
        public static bool Is(this ITypeSymbol typeSymbol, SystemType type)
            => !(typeSymbol is null) && typeSymbol.IsMatch(type);

        public static bool IsInType(this ISymbol symbol, SystemType type)
            => !(symbol is null) && symbol.ContainingType.Is(type);

        private static bool IsMatch(this ITypeSymbol typeSymbol, SystemType type)
            => type.Matches(typeSymbol.SpecialType)
            || type.Matches(typeSymbol.OriginalDefinition.SpecialType)
            || type.Matches(typeSymbol.ToDisplayString())
            || type.Matches(typeSymbol.OriginalDefinition.ToDisplayString());
    }
}
