namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class PreferStronglyTypedIdOverGuid() : CodingRule(Rule.PreferStronglyTypedIdOverGuid)
{
    protected override void Register(AnalysisContext context)
        => RegisterSyntaxNodeAction(context, Report, SyntaxKind.PropertyDeclaration);

    private void Report(SyntaxNodeAnalysisContext context)
    {
        if (context.Node.PropertyDeclaration(context.SemanticModel) is
            {
                IsInstance: true,
                IsObsolete: false,
                IsContractual: false,
                Accessibility: Accessibility.Public,
                DeclaringType.Accessibility: Accessibility.Public,
                Symbol.Type: INamedTypeSymbol type,
            } property
            && IsGuidUuid(type)
            && property.Attributes.None(IsPrimitiveRequired))
        {
            context.ReportDiagnostic(Diagnostic, property.PropertyType);
        }
    }

    private static bool IsPrimitiveRequired(AttributeDecoration decoration)
        => decoration.HasName("PrimitiveRequired");

    private static bool IsGuidUuid(INamedTypeSymbol type)
        => type.IsAny(SystemType.System.Guid, SystemType.Qowaiv.Uuid)
        || type.NotNullable().IsAny(SystemType.System.Guid, SystemType.Qowaiv.Uuid);
}
