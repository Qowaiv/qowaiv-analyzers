namespace Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class TypeSyntaxExtensions
{
    [Pure]
    public static IEnumerable<TypeSyntax> SubTypes(this TypeSyntax? type) => type switch
    {
        ArrayTypeSyntax array => array.ElementType.SubTypes(),
        GenericNameSyntax generic => type.Singleton().Concat(generic.TypeArgumentList.Arguments.SelectMany(SubTypes)),
        _ => type.Singleton(),
    };

    [Pure]
    public static IEnumerable<TypeSyntax> ParameterTypes(this RecordDeclarationSyntax record)
        => record.ParameterList?.Parameters.Select(p => p.Type).OfType<TypeSyntax>() ?? [];
}
