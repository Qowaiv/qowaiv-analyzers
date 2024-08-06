
namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class PreferRegularOverPositionalProperties() : CodingRule(Rule.PreferRegularOverPositionalProperties)
{
    protected override void Register(AnalysisContext context)
        => context.RegisterSyntaxNodeAction(Report, SyntaxKind.RecordDeclaration);

    private void Report(SyntaxNodeAnalysisContext context)
    {
        if (context.Compilation.LanguageVersion() >= LanguageVersionExt.CSharp11
            && context.Node is RecordDeclarationSyntax declaration
            && declaration.ParameterList is { } list)
        {
            foreach (var parameter in list.Parameters)
            {
                context.ReportDiagnostic(Diagnostic, parameter, parameter.Identifier, Message(parameter));
            }
        }
    }

    private static string Message(ParameterSyntax parameter) => parameter switch
    {
        _ when parameter.Default is { } => "a property with a default value",
        _ when NotNull(parameter) => "a required property",
        _ => "a regular property",
    };

    internal static bool NotNull(ParameterSyntax parameter)
        => parameter.Type is not NullableTypeSyntax;
}
