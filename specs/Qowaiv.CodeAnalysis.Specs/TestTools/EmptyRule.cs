#pragma warning disable RS1036 // Specify analyzer banned API enforcement setting

using Qowaiv.CodeAnalysis.Diagnostics;

namespace Specs.TestTools;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
internal sealed class NoRule() : CodingRule(new DiagnosticDescriptor(nameof(NoRule), "No Rule", "Specs", string.Empty, DiagnosticSeverity.Warning, true))
{
    protected override void Register(AnalysisContext context) { }
}
