namespace Qowaiv.CodeAnalysis.Rules;

/// <summary>Implements <see cref="Rule.DecorateValidationAttributes"/>.</summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class DecorateValidationAttributes() : CodingRule(Rule.DecorateValidationAttributes)
{
    /// <inheritdoc />
    protected override void Register(AnalysisContext context)
        => context.RegisterSyntaxNodeAction(Report, SyntaxKind.ClassDeclaration);

    private void Report(SyntaxNodeAnalysisContext context)
    {
        var declaration = context.Node.TypeDeclaration(context.SemanticModel);

        if (declaration.IsConcrete
            && declaration.Symbol.IsAssignableTo(SystemType.System.ComponentModel.DataAnnotations.ValidationAttribute)
            && declaration.Attributes.None(IsValidatesAttribute))
        {
            context.ReportDiagnostic(Diagnostic, declaration.Identifier);
        }
    }

    private static bool IsValidatesAttribute(AttributeDecoration attr)
        => attr.Symbol.Is(SystemType.Qowaiv.Validation.DataAnnotations.ValidatesAttribute);
}
