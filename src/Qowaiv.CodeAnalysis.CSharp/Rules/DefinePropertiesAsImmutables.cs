namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class DefinePropertiesAsImmutables() : CodingRule(Rule.DefinePropertiesAsImmutables)
{
    protected override void Register(AnalysisContext context)
        => context.RegisterSyntaxNodeAction(Report, SyntaxKind.PropertyDeclaration);

    private void Report(SyntaxNodeAnalysisContext context)
    {
        var property = context.Node.PropertyDeclaration(context.SemanticModel);
        if (IsAccessable(property.Accessibility)
            && !property.IsStatic
            && HasSetter(property)
            && !property.IsObsolete
            && IsApplicable(property.DeclaringType)
            && property.TypedNode.AccessorList!.Accessors.FirstOrDefault(a => a.IsKind(SyntaxKind.SetAccessorDeclaration)) is { } accessor)
        {
            context.ReportDiagnostic(Diagnostic, accessor);
        }
    }

    private static bool IsApplicable(TypeDeclaration declaration)
        => !declaration.Modifiers.Contains(SyntaxKind.RefKeyword)
        && !declaration.IsObsolete
        && IsAccessable(declaration.Accessibility)
        && declaration.Symbol is { } declaring
        && !IsDecorated(declaring.GetAttributes());

    [Pure]
    private static bool HasSetter(PropertyDeclaration declaration)
        => declaration.Accessors.Contains(SyntaxKind.SetAccessorDeclaration);

    [Pure]
    private static bool IsAccessable(Accessibility accessibility)
        => accessibility == Accessibility.Protected
        || accessibility == Accessibility.Public;

    [Pure]
    private static bool IsDecorated(IEnumerable<AttributeData> attributes)
     => attributes.Any(attr => IsDecoratedAttribute(attr.AttributeClass!));

    [Pure]
    private static bool IsDecoratedAttribute(INamedTypeSymbol attr)
       => "MUTABLE".Matches(attr.Name)
       || "MUTABLEATTRIBUTE".Matches(attr.Name)
       || (attr.BaseType is { } && IsDecoratedAttribute(attr.BaseType));
}
