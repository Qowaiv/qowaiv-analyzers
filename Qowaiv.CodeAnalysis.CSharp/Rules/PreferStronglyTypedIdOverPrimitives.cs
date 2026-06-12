namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class PreferStronglyTypedIdOverPrimitives() : CodingRule(Rule.PreferStronglyTypedIdOverPrimitives)
{
    protected override void Register(AnalysisContext context)
        => RegisterSyntaxNodeAction(context, Report, SyntaxKind.PropertyDeclaration);

    private void Report(SyntaxNodeAnalysisContext context)
    {
        if (context.Node.PropertyDeclaration(context.SemanticModel) is
            {
                IsInstance: true,
                IsObsolete: false,
                IsOverride: false,
                Accessibility: Accessibility.Public,
                DeclaringType.Accessibility: Accessibility.Public,
                Symbol.Type: INamedTypeSymbol type,
            } property
            && (property.Attributes.Any(IsKey) || HasIdName(property.Name))
            && IsPrimitive(type)
            && property.Attributes.None(IsPrimitiveRequired))
        {
            context.ReportDiagnostic(Diagnostic, property.PropertyType);
        }
    }

    private static bool HasIdName(string name)
        => name.Matches("ID")
        || name.EndsWith("Id")
        || name.EndsWith("ID");

    private static bool IsKey(AttributeDecoration decoration)
        => decoration.HasName("Key")
        || decoration.HasName("PrimaryKey")
        || decoration.HasName("ForeignKey");

    private static bool IsPrimitiveRequired(AttributeDecoration decoration)
        => decoration.HasName("PrimitiveRequired");

    private static bool IsPrimitive(INamedTypeSymbol type) => type.SpecialType
        is SpecialType.System_String
        or SpecialType.System_Int32
        or SpecialType.System_UInt32
        or SpecialType.System_Int64
        or SpecialType.System_UInt64;
}
