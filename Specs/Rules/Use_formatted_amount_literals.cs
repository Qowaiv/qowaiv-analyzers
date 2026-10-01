using Qowaiv.CodeAnalysis.Rules;
using Qowaiv.Financial;

namespace Rules.Use_formatted_amount_literals;

public class Verify
{
    [Test]
    public void Rule_for_classes() => new UseFormattedAmountLiterals()
        .ForCS()
        .AddSource(@"Cases/UseFormattedAmountLiterals.cs")
        .AddReference<Amount>()
        .Verify();
}
