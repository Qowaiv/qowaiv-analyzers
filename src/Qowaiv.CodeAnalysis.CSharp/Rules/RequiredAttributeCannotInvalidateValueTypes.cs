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
                    && type.Is(SystemType.System.ComponentModel.DataAnnotations.RequiredAttribute))
                {
                    context.ReportDiagnostic(Diagnostic, attribute);
                }
            }
        }
    }
}
