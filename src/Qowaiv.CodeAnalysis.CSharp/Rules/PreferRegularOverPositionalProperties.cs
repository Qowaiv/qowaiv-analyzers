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
            && declaration.ParameterList is { } list
            && context.Node.TypeDeclaration(context.SemanticModel) is { IsPublic: true, IsObsolete: false })
        {
            foreach (ParameterSyntax parameter in list.Parameters)
            {
                if (!Contains(declaration.BaseList, parameter))
                {
                    context.ReportDiagnostic(Diagnostic, parameter, parameter.Identifier, Message(parameter));
                }
            }
        }
    }

    private static bool Contains(BaseListSyntax? @base, ParameterSyntax parameter)
        => @base?.Types is { Count: 1 } types
        && types[0] is PrimaryConstructorBaseTypeSyntax primary
        && primary.ArgumentList?.Arguments is { } arguments
        && arguments.Any(a => a.Name() == parameter.Name());

    private static string Message(ParameterSyntax parameter) => parameter switch
    {
        _ when parameter.Default is { } => "a property with a default value",
        _ when NotNull(parameter) => "a required property",
        _ => "a regular property",
    };

    internal static bool NotNull(ParameterSyntax parameter)
        => parameter.Type is not NullableTypeSyntax;
}
