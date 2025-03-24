namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class DefineOnlyOneRequiredAttribute() : CodingRule(Rule.DefineOnlyOneRequiredAttribute)
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

        var found = 0;

        foreach (var attribute in member.Attributes)
        {
            if (attribute.Symbol is { } type
                && type.IsAssignableTo(SystemType.System.ComponentModel.DataAnnotations.RequiredAttribute)
                && ++found > 1)
            {
                context.ReportDiagnostic(Diagnostic, attribute, member.Name());
            }
        }
    }
}
