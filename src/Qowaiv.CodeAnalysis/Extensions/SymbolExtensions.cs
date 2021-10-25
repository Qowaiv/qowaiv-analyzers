using Qowaiv.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microsoft.CodeAnalysis
{
    public static class SymbolExtensions
    {
        public static bool Is(this ITypeSymbol typeSymbol, SystemType type)
            => !(typeSymbol is null) && typeSymbol.IsMatch(type);

        public static bool MemberOf(this ISymbol symbol, SystemType type)
            => !(symbol is null) && symbol.ContainingType.Is(type);

        public static bool IsPublic(this ISymbol symbol) => symbol.DeclaredAccessibility == Accessibility.Public;

        public static string GetFullMetadataName(this ISymbol symbol)
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

        public static MethodInfo GetMethodInfo(this IMethodSymbol symbol)
        {
            if (MethodInfos.TryGetValue(symbol, out var method))
            {
                return method;
            }
            else
            {
                lock (Locker)
                {
                    if (!MethodInfos.TryGetValue(symbol, out method))
                    {
                        var type = symbol.ContainingType.GetRuntimeType();
                        var bindings = symbol.IsStatic ? BindingFlags.Static : BindingFlags.Instance;
                        bindings |= symbol.IsPublic() ? BindingFlags.Public : BindingFlags.NonPublic;
                        var methods = type.GetMethods(bindings);
                        method = methods.First(m
                            => m.Name == symbol.Name
                            && m.ReturnType == symbol.ReturnType.GetRuntimeType()
                            && ParametersMatch(m.GetParameters(), symbol.Parameters));
                        MethodInfos[symbol] = method;
                    }
                    return method;
                }
            }

            static bool ParametersMatch(ParameterInfo[] infos, ImmutableArray<IParameterSymbol> symbols)
            {
                if (infos.Length != symbols.Length) return false;
                else
                {
                    for (var index = 0; index < infos.Length; index++)
                    {
                        if (infos[index].ParameterType != symbols[index].Type.GetRuntimeType())
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
        }

        public static Assembly GetAssembly(this ISymbol symbol)
            => symbol is IAssemblySymbol assemblySymbol
            ? Assembly.Load(assemblySymbol?.MetadataName)
            : Assembly.Load(symbol.ContainingAssembly?.MetadataName);

        public static Type GetRuntimeType(this ITypeSymbol symbol)
        {
            if (symbol.SpecialType.GetRuntimeType() is { } type
                || RuntimeTypes.TryGetValue(symbol, out type))
            {
                return type;
            }
            else
            {
                lock (Locker)
                {
                    if (!RuntimeTypes.TryGetValue(symbol, out type))
                    {
                        type = symbol.GetAssembly().GetType(symbol.GetFullMetadataName());
                        type ??= Type.GetType(symbol.GetFullMetadataName());
                        RuntimeTypes[symbol] = type;
                    }
                    return type;
                }
            }
        }

        private static bool IsMatch(this ITypeSymbol typeSymbol, SystemType type)
            => type.Matches(typeSymbol.SpecialType)
            || type.Matches(typeSymbol.OriginalDefinition.SpecialType)
            || type.Matches(typeSymbol.ToDisplayString())
            || type.Matches(typeSymbol.OriginalDefinition.ToDisplayString());

        private static bool IsRootNamespace(this ISymbol symbol)
            => symbol is INamespaceSymbol ns
            && ns.IsGlobalNamespace;

        private static readonly object Locker = new object();
        private static readonly Dictionary<ISymbol, Type> RuntimeTypes = new(SymbolEqualityComparer.Default);
        private static readonly Dictionary<IMethodSymbol, MethodInfo> MethodInfos = new(SymbolEqualityComparer.Default);
    }
}
