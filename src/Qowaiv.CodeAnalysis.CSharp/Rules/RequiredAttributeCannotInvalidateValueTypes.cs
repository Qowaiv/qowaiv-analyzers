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
                    && type.Is(SystemType.System_ComponentModel_DataAnnotations_RequiredAttribute))
                {
                    context.ReportDiagnostic(Diagnostic, attribute);
                }
            }
        }
    }
}
