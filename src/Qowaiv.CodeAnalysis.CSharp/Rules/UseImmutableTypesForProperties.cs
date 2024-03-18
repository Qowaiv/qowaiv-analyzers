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
            && !symbol.Name.StartsWith("Immutable")
            && IsMutable(symbol));

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
    private static bool DefinesMutableInterface(ITypeSymbol type)
        => type.IsAny(
            SystemType.System_Collections_Generic_ICollection_T,
            SystemType.System_Collections_Generic_IDictionary_TKey_TValue,
            SystemType.System_Collections_Generic_IList_T,
            SystemType.System_Collections_Generic_ISet_T);
}
