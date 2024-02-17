namespace Microsoft.CodeAnalysis;

internal static class SymbolExtensions
{
    [Pure]
    public static bool Equals(this ITypeSymbol type, ITypeSymbol other, bool IncludeNullability)
        => type.Equals(other, IncludeNullability ? SymbolEqualityComparer.IncludeNullability : SymbolEqualityComparer.Default);

    [Pure]
    public static bool IsNot(this ITypeSymbol symbol, SystemType type)
        => !symbol.Is(type);

    [Pure]
    public static bool Is(this ITypeSymbol? symbol, SystemType type)
        => symbol is { } && symbol.IsMatch(type);

    [Pure]
    public static bool IsAssignableTo(this ITypeSymbol? symbol, SystemType type)
        => (symbol is { } && symbol.IsMatch(type))
        || (symbol?.BaseType is { } @base && @base.IsAssignableTo(type));

    [Pure]
    public static bool IsAttribute(this ITypeSymbol type)
        => type.IsAssignableTo(SystemType.System_Attribute);

    [Pure]
    public static bool IsException(this ITypeSymbol type)
        => type.IsAssignableTo(SystemType.System_Exception);

    [Pure]
    public static bool IsNullableValueType(this ITypeSymbol type)
        => type.IsValueType
        && type is { SpecialType: SpecialType.System_Nullable_T } or { OriginalDefinition.SpecialType: SpecialType.System_Nullable_T };

    [Pure]
    public static bool IsObsolete(this ITypeSymbol type)
        => type.GetAttributes().Any(attr => attr.AttributeClass.Is(SystemType.System_ObsoleteAttribute));

    [Pure]
    public static bool IsObsolete(this IMethodSymbol method)
        => method.GetAttributes().Any(attr => attr.AttributeClass.Is(SystemType.System_ObsoleteAttribute))
        || method.ContainingType.IsObsolete();

    [Pure]
    public static bool IsObsolete(this IPropertySymbol method)
        => method.GetAttributes().Any(attr => attr.AttributeClass.Is(SystemType.System_ObsoleteAttribute))
        || method.ContainingType.IsObsolete();

    [Pure]
    public static bool IsPublic(this ISymbol symbol) => symbol.DeclaredAccessibility == Accessibility.Public;

    [Pure]
    public static bool IsProtected(this ISymbol symbol) => symbol.DeclaredAccessibility == Accessibility.Protected;

    [Pure]
    public static bool MemberOf(this ISymbol? symbol, SystemType type)
        => symbol is { } && symbol.ContainingType.Is(type);

    [Pure]
    public static string GetFullMetaDataName(this ISymbol symbol)
    {
        if (symbol is null || symbol.IsRootNamespace())
        {
            return string.Empty;
        }
        else
        {
            var sb = new StringBuilder(symbol.MetadataName);
            var previous = symbol;

            symbol = symbol.ContainingSymbol;

            while (!symbol.IsRootNamespace())
            {
                var connector = symbol is ITypeSymbol && previous is ITypeSymbol ? '+' : '.';
                sb.Insert(0, connector);
                sb.Insert(0, symbol.OriginalDefinition.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat));
                symbol = symbol.ContainingSymbol;
            }
            return sb.ToString();
        }
    }

    [Pure]
    public static INamedTypeSymbol? NotNullable(this INamedTypeSymbol type)
        => type.IsNullableValueType()
        && type.TypeArguments[0] is INamedTypeSymbol inner
            ? inner
            : null;

    [Pure]
    private static bool IsMatch(this ITypeSymbol typeSymbol, SystemType type)
        => type.Matches(typeSymbol.SpecialType)
        || type.Matches(typeSymbol.OriginalDefinition.SpecialType)
        || type.Matches(typeSymbol.ToDisplayString())
        || type.Matches(typeSymbol.OriginalDefinition.ToDisplayString());

    [Pure]
    private static bool IsRootNamespace(this ISymbol symbol)
        => symbol is INamespaceSymbol ns
        && ns.IsGlobalNamespace;
}
