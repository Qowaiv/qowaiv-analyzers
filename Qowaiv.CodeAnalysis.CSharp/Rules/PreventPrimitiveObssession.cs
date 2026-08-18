namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class PreventPrimitiveObssession() : CodingRule(
    Rule.PreferStronglyTypedIdOverGuid,
    Rule.PreferStronglyTypedIdOverPrimitives)
{
    protected override void Register(AnalysisContext context)
       => RegisterSyntaxNodeAction(context, Report, SyntaxKind.PropertyDeclaration);

    private void Report(SyntaxNodeAnalysisContext context)
    {
        if (context.Node.PropertyDeclaration(context.SemanticModel) is not
            {
                IsInstance: true,
                IsObsolete: false,
                IsContractual: false,
                Accessibility: Accessibility.Public,
                DeclaringType.Accessibility: Accessibility.Public,
                Symbol.Type: INamedTypeSymbol type,
            } property
            || property.Attributes.Any(PrimitiveIsRequired))
        {
            return;
        }

        if (PreferStronglyTypedIdOverGuid(type))
        {
            context.ReportDiagnostic(Rule.PreferStronglyTypedIdOverGuid, property.PropertyType);
        }
        else if (PreferStronglyTypedIdOverPrimitives(property, type))
        {
            context.ReportDiagnostic(Rule.PreferStronglyTypedIdOverPrimitives, property.PropertyType);
        }
    }

    private static bool PreferStronglyTypedIdOverGuid(INamedTypeSymbol type)
       => type.IsAny(SystemType.System.Guid, SystemType.Qowaiv.Uuid)
       || type.NotNullable.IsAny(SystemType.System.Guid, SystemType.Qowaiv.Uuid);

    private static bool PreferStronglyTypedIdOverPrimitives(PropertyDeclaration property, INamedTypeSymbol type)
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

    private static bool PrimitiveIsRequired(AttributeDecoration decoration)
        => decoration.HasName("PrimitiveRequired");
}
