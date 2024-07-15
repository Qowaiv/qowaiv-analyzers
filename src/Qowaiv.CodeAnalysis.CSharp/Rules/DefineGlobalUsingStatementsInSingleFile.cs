namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class DefineGlobalUsingStatementsInSingleFile(string globalConfigFile)
    : DefineGlobalUsingStatements(Rule.DefineGlobalUsingStatementsInSingleFile)
{
    public DefineGlobalUsingStatementsInSingleFile() : this("Properties/GlobalUsings.cs") { }

    private readonly string GlobalUsingsFile = globalConfigFile;

    protected override void Report(SyntaxNodeAnalysisContext context)
    {
        if (IsGlobalDirective(context.Node))
        {
            var globalConfig = context.TryGetConfiguredProperty(Diagnostic, nameof(GlobalUsingsFile)) ?? GlobalUsingsFile;

            if (IsDifferentFile(context.Node.SyntaxTree.FilePath, globalConfig))
            {
                context.ReportDiagnostic(Diagnostic, context.Node, globalConfig);
            }
        }
    }

    private static bool IsDifferentFile(string? filepath, string globalConfig)
        => Split(filepath) is not { Length: > 0 } splitted
        || !EndsWith(Split(globalConfig), splitted);

    private static bool EndsWith(string[] globalConfig, string[] filePath)
        => globalConfig.Length <= filePath.Length
        && globalConfig.SequenceEqual(filePath.Skip(filePath.Length - globalConfig.Length));

    private static string[] Split(string? path) => (path ?? string.Empty).Split('/', '\\');
}
