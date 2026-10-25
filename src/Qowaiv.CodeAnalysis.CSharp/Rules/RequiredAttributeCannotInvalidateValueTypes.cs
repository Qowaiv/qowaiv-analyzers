using System.Linq.Expressions;

namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class RequiredAttributeCannotInvalidateValueTypes() : CodingRule(Rule.RequiredCannotInvalidateValueTypes)
{
    protected override void Register(AnalysisContext context)
     => RegisterSyntaxNodeAction(
            context,
            Report,
            SyntaxKind.PropertyDeclaration,
            SyntaxKind.FieldDeclaration,
            SyntaxKind.Parameter);

    private void Report(SyntaxNodeAnalysisContext context)
    {
        var member = context.Node.MemberDeclaration(context.SemanticModel);

        if (member.Attributes.Any() && member.Symbol is { IsValueType: true, IsNullableValueType: false })
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
        => type?.GetAttributes().Any(IsJsonAttribute) is true
        || type.Properties.Any(p => p.GetAttributes().Any(IsJsonAttribute));

    private static bool IsJsonAttribute(AttributeData attr)
        => attr.AttributeClass.FullMetaDataName is { Length: > 0 } name
        && (name.StartsWith("System.Text.Json.Serialization.")
        || name.StartsWith("Newtonsoft.Json."));
}
