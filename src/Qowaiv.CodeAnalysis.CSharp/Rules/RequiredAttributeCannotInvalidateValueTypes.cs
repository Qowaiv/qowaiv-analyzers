namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class RequiredAttributeCannotInvalidateValueTypes() : CodingRule(Rule.RequiredCannotInvalidateValueTypes)
{
    protected override void Register(AnalysisContext context)
     => context.RegisterSyntaxNodeAction(
            Report,
            SyntaxKind.PropertyDeclaration,
            SyntaxKind.FieldDeclaration,
            SyntaxKind.Parameter);

    private void Report(SyntaxNodeAnalysisContext context)
    {
        var member = context.Node.MemberDeclaration(context.SemanticModel);

        if (member.Attributes.Any() && member.Symbol is { } symbol && symbol.IsNotNullableValueType())
        {
            foreach (var attribute in member.Attributes)
            {
                if (attribute.Symbol is { } type
                    && type.Is(SystemType.System.ComponentModel.DataAnnotations.RequiredAttribute)
                    && !DecoratedWithJsonAttribute(context))
                {
                    context.ReportDiagnostic(Diagnostic, attribute);
                }
            }
        }
    }

    private static bool DecoratedWithJsonAttribute(SyntaxNodeAnalysisContext context)
        => context.Node.Parent?.TryTypeDeclaration(context.SemanticModel) is { } parent
        && DecoratedWithJsonAttribute(parent.Symbol);

    private static bool DecoratedWithJsonAttribute(INamedTypeSymbol? type)
        => type?.GetAttributes().Any(IsJsonAsstribute) is true
        || type?.GetProperties().Any(p => p.GetAttributes().Any(IsJsonAsstribute)) is true;

    private static bool IsJsonAsstribute(AttributeData attr)
        => attr.AttributeClass?.GetFullMetaDataName() is { Length: > 0 } name
        && (name.StartsWith("System.Text.Json.Serialization.")
        || name.StartsWith("Newtonsoft.Json."));
}
