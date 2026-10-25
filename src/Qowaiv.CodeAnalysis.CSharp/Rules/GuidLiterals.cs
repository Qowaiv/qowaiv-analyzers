namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class GuidLiterals() : GuidLiteralsBase(
    Rule.GuidLiteralsMustBeCompliant,
    Rule.GuidLiteralsShouldBeProvided,
    Rule.UseGuidParseOrEmpty)
{
    protected override SystemType Type => SystemType.System.Guid;

    protected override bool IsValid(string literal) => Guid.TryParse(literal, out _);
}
