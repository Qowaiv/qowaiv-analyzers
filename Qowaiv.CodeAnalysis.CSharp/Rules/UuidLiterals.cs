namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UuidLiterals() : GuidLiteralsBase(
    Rule.UuidLiteralsMustBeCompliant,
    Rule.UuidLiteralsShouldBeProvided,
    Rule.UseUuidParseOrEmpty)
{
    protected override SystemType Type => SystemType.Qowaiv.Uuid;

    protected override bool InvalidGuid(string literal) => !UUID.IsValid(literal);
}
