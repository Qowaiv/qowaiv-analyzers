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
                IsOverride: false,
                Accessibility: Accessibility.Public,
                DeclaringType.Accessibility: Accessibility.Public,
                Symbol.Type: INamedTypeSymbol type,
            } property
            && IsGuidUuid(type))
        {
            context.ReportDiagnostic(Diagnostic, property.PropertyType);
        }
    }

    private static bool IsGuidUuid(INamedTypeSymbol type)
        => type.IsAny(SystemType.System.Guid, SystemType.Qowaiv.Uuid)
        || type.NotNullable().IsAny(SystemType.System.Guid, SystemType.Qowaiv.Uuid);
}
