using Qowaiv.CodeAnalysis.CodeFixes;
using Qowaiv.CodeAnalysis.Rules;

namespace Fixes.Round_double_after_conversion_to_decimal;

public class Fixes
{
    [Test]
    public void Code()
        => new RoundDoubleAfterConversionToDecimal()
        .ForCS()
        .AddSource(@"Cases/RoundDoubleAfterConversionToDecimal.ToFix.cs")
        .ForCodeFix<UseDecimalLiteral>()
        .AddSource(@"Cases/RoundDoubleAfterConversionToDecimal.Fixed.cs")
        .Verify();
}
