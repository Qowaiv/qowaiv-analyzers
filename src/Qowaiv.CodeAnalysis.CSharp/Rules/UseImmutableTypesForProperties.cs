using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UseImmutableTypesForProperties() : CodingRule(Rule.UseImmutableTypesForProperties)
{
    protected override void Register(AnalysisContext context)
        => context.RegisterSyntaxNodeAction(Report, SyntaxKind.PropertyDeclaration);

    private void Report(SyntaxNodeAnalysisContext context)
    {
        var property = context.Node.PropertyDeclaration(context.SemanticModel);
        if (IsAccessable(property.Accessibility)
            && !property.IsStatic
            && !property.IsObsolete
            && IsApplicable(property.DeclaringType)
            && IsMutable(property.PropertyType))
        {
            context.ReportDiagnostic(Diagnostic, property.PropertyType);
        }
    }

    [Pure]
    private bool IsMutable(TypeNode type)
        => type.IsArray
        || (type.Symbol is { } symbol && IsMutable(symbol));

    [Pure]
    private bool IsMutable(INamedTypeSymbol type, Dictionary<ITypeSymbol, bool>? todo = null)
    {
        if (Mutability.TryGetValue(type, out var mutability))
        {
            return mutability;
        }
        else
        {
            var next = new Dictionary<ITypeSymbol, bool>(todo ?? [], SymbolEqualityComparer.Default);

            foreach (var sub in type.SelfAndAncestorTypes()
                .Concat(type.AllInterfaces)
                .Concat(type.TypeArguments)
                .Concat(AccessableProperties(type).Select(p => p.Type)))
            {
                next[sub] = false;

                if (sub.SpecialType != SpecialType.None || sub.IsReadOnly)
                {
                    Mutability[sub] = false;
                }
                else if (Mutability.TryGetValue(sub, out mutability) && mutability)
                {
                    return true;
                }
                else if (TestMutable(sub))
                {
                    return Mutability[sub] = true;
                }
                else if (!SymbolEqualityComparer.Default.Equals(sub, type) && !next.ContainsKey(sub))
                {
                    next[sub] = true;
                }
            }

            if (next.Values.Any(v => v))
            {
                return next.Where(kvp => kvp.Value).Select(kvp => kvp.Key).OfType<INamedTypeSymbol>().Any(t => IsMutable(t, next));
            }
        }
        return Mutability[type] = false;
    }

    [Pure]
    private static bool TestMutable(ITypeSymbol type)
        => IsDecorated(type.GetAttributes())
        || DefinesMutableInterface(type)
        || HasEditableProperty(type);

    [Pure]
    private static bool HasEditableProperty(ITypeSymbol type) 
        => AccessableProperties(type).Any(p => p.SetMethod is { IsInitOnly: false });

    [Pure]
    private static IEnumerable<IPropertySymbol> AccessableProperties(ITypeSymbol type)
        => type.GetProperties().Where(p => !p.IsStatic && IsAccessable(p.DeclaredAccessibility));

    [Pure]
    private static bool DefinesMutableInterface(ITypeSymbol type)
        => type.IsAny(
            SystemType.System_Collections_Generic_ICollection_T,
            SystemType.System_Collections_Generic_IDictionary_TKey_TValue,
            SystemType.System_Collections_Generic_IList_T,
            SystemType.System_Collections_Generic_ISet_T);

    [Pure]
    private static bool IsApplicable(TypeDeclaration declaration)
        => !declaration.Modifiers.Contains(SyntaxKind.RefKeyword)
        && !declaration.IsObsolete
        && IsAccessable(declaration.Accessibility)
        && declaration.Symbol is { } declaring
        && !IsDecorated(declaring.GetAttributes());

    [Pure]
    private static bool IsDecorated(IEnumerable<AttributeData> attributes)
    => attributes.Any(attr => IsDecoratedAttribute(attr.AttributeClass!));

    [Pure]
    private static bool IsAccessable(Accessibility accessibility)
        => accessibility == Accessibility.Protected
        || accessibility == Accessibility.Public;

    [Pure]
    private static bool IsDecoratedAttribute(ITypeSymbol attr)
        => "MUTABLE".Matches(attr.Name)
        || "MUTABLEATTRIBUTE".Matches(attr.Name)
        || (attr.BaseType is { } && IsDecoratedAttribute(attr.BaseType));

    private ConcurrentDictionary<ITypeSymbol, bool> Mutability = new(SymbolEqualityComparer.Default);
}
