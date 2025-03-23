namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class DefineOnlyOneRequiredAttribute() : CodingRule(Rule.DefineOnlyOneRequiredAttribute)
{
    protected override void Register(AnalysisContext context)
        => context.RegisterSyntaxNodeAction(Report, SyntaxKind.PropertyDeclaration);

    private void Report(SyntaxNodeAnalysisContext context)
    {
        var property = context.Node.PropertyDeclaration(context.SemanticModel);

        var found = 0;

        foreach (var attribute in property.Attributes)
        {
            if (context.SemanticModel.GetSymbolInfo(attribute).Symbol is IMethodSymbol symbol
                && symbol.ReceiverType is INamedTypeSymbol type
                && type.IsAssignableTo(SystemType.System_ComponentModel_DataAnnotations_RequiredAttribute)
                && ++found > 1)
            {
                context.ReportDiagnostic(Diagnostic, attribute, property.Name());
            }
        }
    }
}
