using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Qowaiv.CodeAnalysis.Rules;

namespace Qowaiv.CodeAnalysis.CSharp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class ParseShouldNotFail : ParseShouldNotFail<SyntaxKind> { }
}
