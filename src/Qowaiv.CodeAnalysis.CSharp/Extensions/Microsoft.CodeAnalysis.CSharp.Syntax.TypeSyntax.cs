namespace Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class TypeSyntaxExtensions
{
    /// <summary>Maps C# predefined type keywords to the short name of the type they bind to.</summary>
    private static readonly FrozenDictionary<string, string> PredefinedTypeNames = new Dictionary<string, string>()
    {
        ["bool"] = "Boolean",
        ["byte"] = "Byte",
        ["sbyte"] = "SByte",
        ["char"] = "Char",
        ["decimal"] = "Decimal",
        ["double"] = "Double",
        ["float"] = "Single",
        ["int"] = "Int32",
        ["uint"] = "UInt32",
        ["long"] = "Int64",
        ["ulong"] = "UInt64",
        ["object"] = "Object",
        ["short"] = "Int16",
        ["ushort"] = "UInt16",
        ["string"] = "String",
        ["void"] = "Void",
    }
    .ToFrozenDictionary(StringComparer.Ordinal);

    [Pure]
    public static IEnumerable<TypeSyntax> SubTypes(this TypeSyntax? type) => type switch
    {
        null => [],
        ArrayTypeSyntax array => array.ElementType.SubTypes(),
        GenericNameSyntax generic => [type, .. generic.TypeArgumentList.Arguments.SelectMany(SubTypes)],
        _ => [type],
    };

    /// <summary>
    /// The identifier a type reference is written with: the rightmost part of the
    /// name, or the type the keyword stands for. Null if that can not be
    /// determined syntactically.
    /// </summary>
    /// <remarks>
    /// A type can only be referred to by its own short name, or through a using
    /// alias (as the alias identifier is free to chose). This makes it possible
    /// to rule references out on strings alone.
    /// </remarks>
    [Pure]
    public static string? SimpleName(this TypeSyntax? type) => type switch
    {
        SimpleNameSyntax simple => simple.Identifier.ValueText,
        QualifiedNameSyntax qualified => qualified.Right.SimpleName(),
        AliasQualifiedNameSyntax alias => alias.Name.SimpleName(),
        NullableTypeSyntax nullable => nullable.ElementType.SimpleName(),
        PointerTypeSyntax pointer => pointer.ElementType.SimpleName(),
        ArrayTypeSyntax array => array.ElementType.SimpleName(),
        PredefinedTypeSyntax predefined => PredefinedTypeNames.TryGetValue(predefined.Keyword.ValueText, out var name) ? name : null,
        _ => null,
    };

    /// <summary>Returns true if any identifier of the type reference mentions the token.</summary>
    [Pure]
    public static bool Mentions(this TypeSyntax? type, string token)
        => type?.DescendantTokens().Any(t => t.ValueText.Contains(token)) is true;

    [Pure]
    public static IEnumerable<TypeSyntax> ParameterTypes(this RecordDeclarationSyntax record)
        => record.ParameterList?.Parameters.Select(p => p.Type).OfType<TypeSyntax>() ?? [];
}
