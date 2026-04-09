namespace Qowaiv.CodeAnalysis.Rules;

/// <summary>Implements <see cref="Rule.UseValidatesAttributeOnValidationAttributesOnly"/>.</summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UseValidatesAttributeOnValidationAttributesOnly()
        : CodingRule(Rule.UseValidatesAttributeOnValidationAttributesOnly)
{
    /// <inheritdoc />
    protected override void Register(AnalysisContext context)
        => context.RegisterSyntaxNodeAction(Report, SyntaxKind.ClassDeclaration, SyntaxKind.RecordDeclaration);

    private void Report(SyntaxNodeAnalysisContext context)
    {
        var declaration = context.Node.TypeDeclaration(context.SemanticModel);

        if (!declaration.Symbol.IsAssignableTo(SystemType.System.ComponentModel.DataAnnotations.ValidationAttribute)
            && declaration.Attributes.Any(IsValidatesAttribute))
        {
            context.ReportDiagnostic(Diagnostic, declaration.Identifier);
        }
    }

    private static bool IsValidatesAttribute(AttributeDecoration attr)
        => attr.Symbol.Is(SystemType.Qowaiv.Validation.DataAnnotations.ValidatesAttribute);
}
