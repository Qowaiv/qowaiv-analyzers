using Qowaiv.CodeAnalysis.CodeFixes;
using Qowaiv.CodeAnalysis.Rules;
using Qowaiv.CodeAnalysis.Shared;

namespace Fixes.Format_amount_literal;

public class Fixes
{
    [Test]
    public void Code()
        => new FormatAmountLiterals()
        .ForCS()
        .AddReference<Qowaiv.Financial.Amount>()
        .AddSource(@"Cases/FormatAmountLiterals.ToFix.cs")
        .ForCodeFix<FormatAmountLiteral>()
        .AddSource(@"Cases/FormatAmountLiterals.Fixed.cs")
        .Verify();
}
