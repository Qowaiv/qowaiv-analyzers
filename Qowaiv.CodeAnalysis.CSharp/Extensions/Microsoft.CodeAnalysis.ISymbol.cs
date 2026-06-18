namespace Microsoft.CodeAnalysis;

internal static class SymbolExtensions
{
    extension(ISymbol symbol)
    {
        public bool IsPublic => symbol.DeclaredAccessibility == Accessibility.Public;

        public bool IsProtected => symbol.DeclaredAccessibility == Accessibility.Protected;

        // https://github.com/dotnet/roslyn/blob/2a594fa2157a734a988f7b5dbac99484781599bd/src/Workspaces/SharedUtilitiesAndExtensions/Compiler/Core/Extensions/ISymbolExtensions.cs#L93
        [ExcludeFromCodeCoverage]
        public ImmutableArray<ISymbol> ExplicitOrImplicitInterfaceImplementations
        {
            get
            {
                if (symbol.Kind is not SymbolKind.Method and not SymbolKind.Property and not SymbolKind.Event)
                {
                    return [];
                }

                var containingType = symbol.ContainingType;

                var query =
                    from iface in containingType.AllInterfaces
                    from interfaceMember in iface.GetMembers()
                    let impl = containingType.FindImplementationForInterfaceMember(interfaceMember)
                    where SymbolEqualityComparer.Default.Equals(symbol, impl)
                    select interfaceMember;

                return [.. query];
            }
        }

        internal bool IsRootNamespace
            => symbol is INamespaceSymbol ns
            && ns.IsGlobalNamespace;
    }

    extension(ISymbol? symbol)
    {
        [Pure]
        public bool MemberOf(SystemType type)
            => symbol is { } && symbol.ContainingType.Is(type);

        [Pure]
        public string FullMetaDataName
        {
            get
            {
                if (symbol is null || symbol.IsRootNamespace)
                {
                    return string.Empty;
                }
                else
                {
                    var sb = new StringBuilder(symbol.MetadataName);
                    var previous = symbol;

                    symbol = symbol.ContainingSymbol;

                    while (!symbol.IsRootNamespace)
                    {
                        var connector = symbol is ITypeSymbol && previous is ITypeSymbol ? '+' : '.';
                        sb.Insert(0, connector);
                        sb.Insert(0, symbol.OriginalDefinition.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat));
                        symbol = symbol.ContainingSymbol;
                    }
                    return sb.ToString();
                }
            }
        }
    }

    extension(ITypeSymbol type)
    {
        public bool IsAttribute
        => type.IsAssignableTo(SystemType.System.Attribute);

        public bool IsException
            => type.IsAssignableTo(SystemType.System.Exception);

        public bool IsNullableValueType
            => type.IsValueType
            && type is { SpecialType: SpecialType.System_Nullable_T } or { OriginalDefinition.SpecialType: SpecialType.System_Nullable_T };

        public bool IsObsolete
            => type.GetAttributes().Any(attr => attr.AttributeClass.Is(SystemType.System.ObsoleteAttribute));

        [Pure]
        public bool Equals(ITypeSymbol other, bool includeNullability)
            => type.Equals(other, includeNullability ? SymbolEqualityComparer.IncludeNullability : SymbolEqualityComparer.Default);

        [Pure]
        private bool IsMatch(SystemType other)
            => other.Matches(type.SpecialType)
            || other.Matches(type.OriginalDefinition.SpecialType)
            || other.Matches(type.ToDisplayString())
            || other.Matches(type.OriginalDefinition.ToDisplayString());
    }

    extension(ITypeSymbol? type)
    {
        [Pure]
        public bool Is(SystemType other)
            => type is { } && type.IsMatch(other);

        [Pure]
        public bool IsNot(SystemType other)
            => !type.Is(other);

        [Pure]
        public bool IsAny(params SystemType[] types)
            => Array.Exists(types, type.Is);

        [Pure]
        public bool IsAssignableTo(ITypeSymbol other)
           => SymbolEqualityComparer.IncludeNullability.Equals(type, other)
           || (type?.BaseType is { } @base && @base.IsAssignableTo(other));

        [Pure]
        public bool IsAssignableTo(SystemType other)
            => (type is { } && type.IsMatch(other))
            || (type?.BaseType is { } @base && @base.IsAssignableTo(other));

        [Pure]
        public bool Implements(SystemType other)
            => type is { } && type.AllInterfaces.Any(i => i.Is(other));

        [Pure]
        public IEnumerable<IPropertySymbol> Properties
            => type?.GetMembers().OfType<IPropertySymbol>() ?? [];
    }

    extension(INamedTypeSymbol? type)
    {
        [Pure]
        public IEnumerable<INamedTypeSymbol> SelfAndAncestorTypes()
        {
            var current = type;

            while (current is { })
            {
                yield return current;
                current = current.BaseType;
            }
        }

        [Pure]
        public INamedTypeSymbol? NotNullable
            => type is { IsNullableValueType: true }
            && type.TypeArguments[0] is INamedTypeSymbol inner
                ? inner
                : null;
    }

    extension(IMethodSymbol method)
    {
        public bool IsObsolete
            => method.GetAttributes().Any(attr => attr.AttributeClass.Is(SystemType.System.ObsoleteAttribute))
            || method.ContainingType.IsObsolete;

    }

    extension(IPropertySymbol property)
    {
        public bool IsObsolete
            => property.GetAttributes().Any(attr => attr.AttributeClass.Is(SystemType.System.ObsoleteAttribute))
            || property.ContainingType.IsObsolete;
    }
}
