using Qowaiv.CodeAnalysis.CodeFixes;
using Qowaiv.CodeAnalysis.Rules;
using Qowaiv.CodeAnalysis.Shared;

namespace Fixes.Format_amount;

public class Fixes
{
    [Test]
    public void Code()
        => new UseFormattedAmountLiterals()
        .ForCS()
        .AddReference<Qowaiv.Financial.Amount>()
        .AddSource(@"Cases/UseFormattedAmountLiterals.ToFix.cs")
        .ForCodeFix<FormatAmount>()
        .AddSource(@"Cases/UseFormattedAmountLiterals.Fixed.cs")
        .Verify();
}
