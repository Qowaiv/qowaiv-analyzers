namespace Qowaiv.CodeAnalysis.Rules;

public abstract class PrimitiveObssession(DescriptorContainer supportedDiagnostic) : CodingRule(supportedDiagnostic)
{
    protected abstract bool ShouldNotBePrimitive(PropertyDeclaration property, INamedTypeSymbol type);

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
            && property.Attributes.None(IsPrimitiveRequired)
            && ShouldNotBePrimitive(property, type))
        {
            context.ReportDiagnostic(Diagnostic, property.PropertyType);
        }
    }

    private static bool IsPrimitiveRequired(AttributeDecoration decoration)
        => decoration.HasName("PrimitiveRequired");
}
