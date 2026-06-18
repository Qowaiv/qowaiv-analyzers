namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class PreferStronglyTypedIdOverPrimitives() : PrimitiveObssession(Rule.PreferStronglyTypedIdOverPrimitives)
{
    protected override bool ShouldNotBePrimitive(PropertyDeclaration property, INamedTypeSymbol type)
        => (property.Attributes.Any(IsKey) || HasIdName(property.Name))
        && IsPrimitive(type);

    private static bool HasIdName(string name)
        => name.Matches("ID")
        || name.EndsWith("Id")
        || name.EndsWith("ID");

    private static bool IsKey(AttributeDecoration decoration)
        => decoration.HasName("Key")
        || decoration.HasName("PrimaryKey")
        || decoration.HasName("ForeignKey");

    private static bool IsPrimitive(INamedTypeSymbol type) => type.SpecialType
        is SpecialType.System_String
        or SpecialType.System_Int32
        or SpecialType.System_UInt32
        or SpecialType.System_Int64
        or SpecialType.System_UInt64;
}
