namespace Qowaiv.CodeAnalysis.Rules;

public abstract class ImmutablePropertiesBase(DiagnosticDescriptor supportedDiagnostic)
    : CodingRule(supportedDiagnostic)
{
    [Pure]
    protected static bool IsApplicable(PropertyDeclaration property)
        => IsAccessible(property.Accessibility)
        && !property.IsStatic
        && !property.IsObsolete
        && IsApplicable(property.DeclaringType)
        && HasImmutableBase(property.ContainingSymbol);

    [Pure]
    private static bool IsApplicable(TypeDeclaration declaration)
        => !declaration.Modifiers.Contains(SyntaxKind.RefKeyword)
        && !declaration.IsObsolete
        && IsAccessible(declaration.Accessibility)
        && declaration.Symbol is { } declaring
        && !IsExcluded(declaring)
        && !IsDecorated(declaring.GetAttributes());

    [Pure]
    public static bool IsExcluded(INamedTypeSymbol type)
        => type.Implements(SystemType.System.Collections.IEnumerator)
        || type.Implements(SystemType.System.Xml.Serialization.IXmlSerializable);

    [Pure]
    private static bool HasImmutableBase(INamedTypeSymbol? containing)
    {
        var baseType = containing?.BaseType;
        var immutable = true;
        while (baseType is { })
        {
            if (IsDecorated(baseType.GetAttributes()))
            {
                return true;
            }
            immutable &= !baseType.GetProperties().Any(IsEditable);
            baseType = baseType.BaseType;
        }
        return immutable;
    }

    [Pure]
    private static bool IsEditable(IPropertySymbol property)
        => IsAccessible(property.DeclaredAccessibility)
        && property.SetMethod is { };

    [Pure]
    protected static bool IsAccessible(Accessibility accessibility)
       => accessibility == Accessibility.Protected
       || accessibility == Accessibility.Public;

    [Pure]
    protected static bool IsDecorated(ImmutableArray<AttributeData> attributes)
        => attributes.Any(attr => IsDecoratedAttribute(attr.AttributeClass!));

    [Pure]
    private static bool IsDecoratedAttribute(INamedTypeSymbol attr)
       => "MUTABLE".Matches(attr.Name)
       || "MUTABLEATTRIBUTE".Matches(attr.Name)
       || (attr.BaseType is { } && IsDecoratedAttribute(attr.BaseType));
}
