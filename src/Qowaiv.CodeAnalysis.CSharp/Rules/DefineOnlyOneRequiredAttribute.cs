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
            if (context.SemanticModel.GetSymbolInfo(attribute).Symbol is IMethodSymbol symbol
                && symbol.ReceiverType is INamedTypeSymbol type
                && type.IsAssignableTo(SystemType.System_ComponentModel_DataAnnotations_RequiredAttribute)
                && ++found > 1)
            {
                context.ReportDiagnostic(Diagnostic, attribute, member.Name());
            }
        }
    }
}
