using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.VisualBasic;
using Qowaiv.CodeAnalysis.Rules;

namespace Qowaiv.CodeAnalysis.VisualBasic
{
    [DiagnosticAnalyzer(LanguageNames.VisualBasic)]
    public sealed class ParseShouldNotFail : ParseShouldNotFail<SyntaxKind> { }
}
