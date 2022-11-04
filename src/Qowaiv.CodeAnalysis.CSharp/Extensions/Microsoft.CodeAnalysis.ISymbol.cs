﻿using System.IO;
using System.Reflection;

namespace Microsoft.CodeAnalysis;

internal static class SymbolExtensions
{
    [Pure]
    public static bool IsNot(this ITypeSymbol symbol, SystemType type)
        => !symbol.Is(type);

    [Pure]
    public static bool Is(this ITypeSymbol? symbol, SystemType type)
        => symbol is { } && symbol.IsMatch(type);

    [Pure]
    public static bool IsAssignableTo(this ITypeSymbol? symbol, SystemType type)
        => symbol is { } && symbol.IsMatch(type)
        || (symbol?.BaseType is { } @base && @base.Is(type));

    [Pure]
    public static bool IsAttribute(this ITypeSymbol type)
        => type.IsAssignableTo(SystemType.System_Attribute);

    [Pure]
    public static bool IsException(this ITypeSymbol type)
        => type.IsAssignableTo(SystemType.System_Exception);

    [Pure]
    public static bool IsObsolete(this ITypeSymbol type)
        => type.GetAttributes().Any(attr => attr.AttributeClass.Is(SystemType.System_ObsoleteAttribute));

    [Pure]
    public static bool IsObsolete(this IMethodSymbol method)
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

    [Pure]
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
                if (!MethodInfos.TryGetValue(symbol, out method)
                    && symbol.ContainingType.GetRuntimeType() is { } type)
                {
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

    [Pure]
    public static Assembly? GetAssembly(this ISymbol symbol)
        => symbol is IAssemblySymbol assemblySymbol
        ? LoadAssembly(assemblySymbol)
        : LoadAssembly(symbol.ContainingAssembly);

    [Pure]
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
                if (!RuntimeTypes.TryGetValue(symbol, out type)
                    && symbol.GetAssembly() is { } assembly)
                {
                    var fullName = symbol.GetFullMetadataName();
                    type = assembly.GetTypes().FirstOrDefault(t => t.FullName == fullName);
                    RuntimeTypes[symbol] = type;
                }
                return type;
            }
        }
    }

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

    [Pure]
    private static Assembly? LoadAssembly(IAssemblySymbol symbol)
    {
        var id = symbol.Identity;
        return AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == id.Name)
            ?? Load(symbol)
            ?? LoadFile(id);

        static Assembly? Load(IAssemblySymbol symbol)
        {
            try { return Assembly.Load(symbol.MetadataName); }
            catch { return null; }
        }
        static Assembly? LoadFile(AssemblyIdentity id)
        {
            foreach (var root in AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => !a.IsDynamic)
                .Select(a => Path.GetDirectoryName(a.Location))
                .Distinct())
            {
                try { return Assembly.LoadFile(Path.Combine(root, $"{id.Name}.dll")); }
                catch { }
            }
            return null;
        }
    }

    private static readonly object Locker = new();
    private static readonly Dictionary<ISymbol, Type> RuntimeTypes = new(SymbolEqualityComparer.Default);
    private static readonly Dictionary<IMethodSymbol, MethodInfo> MethodInfos = new(SymbolEqualityComparer.Default);
}
