namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UseImmutableTypesForProperties() : ImmutablePropertiesBase(Rule.UseImmutableTypesForProperties)
{
    protected override void Register(AnalysisContext context)
        => context.RegisterSyntaxNodeAction(Report, SyntaxKind.PropertyDeclaration);

    private void Report(SyntaxNodeAnalysisContext context)
    {
        var property = context.Node.PropertyDeclaration(context.SemanticModel);
        if (IsApplicable(property)
            && IsMutable(property.PropertyType))
        {
            context.ReportDiagnostic(Diagnostic, property.PropertyType);
        }
    }

    [Pure]
    private static bool IsMutable(TypeNode type)
        => type.IsArray
        || (type.Symbol is { } symbol
            && !ExcludeType(symbol)
            && IsMutable(symbol));

    [Pure]
    private static bool ExcludeType(INamedTypeSymbol symbol)
        => symbol.Name.StartsWith("Immutable")
        || symbol.ContainingNamespace.GetFullMetaDataName() == "System.Collections.Frozen";

    [Pure]
    private static bool IsMutable(INamedTypeSymbol type) => type
        .SelfAndAncestorTypes()
        .Concat(type.AllInterfaces)
        .Concat(type.TypeArguments)
        .Concat(AccessableProperties(type).Select(p => p.Type))
        .Any(t => IsDecorated(t.GetAttributes()) || DefinesMutableInterface(t));

    [Pure]
    private static IEnumerable<IPropertySymbol> AccessableProperties(ITypeSymbol type)
        => type.GetProperties().Where(p => !p.IsStatic && IsAccessible(p.DeclaredAccessibility));

    [Pure]
    private static bool DefinesMutableInterface(ITypeSymbol type) => type.IsAny(
        SystemType.System.Collections.Generic.ICollection_T,
        SystemType.System.Collections.Generic.IDictionary_TKey_TValue,
        SystemType.System.Collections.Generic.IList_T,
        SystemType.System.Collections.Generic.ISet_T);
}
