using Qowaiv.CodeAnalysis.Rules;
using Qowaiv.Financial;

namespace Rules.Format_amount_literals;

public class Verify
{
    [Test]
    public void Rule_for_classes() => new FormatAmountLiterals()
        .ForCS()
        .AddSource(@"Cases/FormatAmountLiterals.cs")
        .AddReference<Amount>()
        .Verify();
}
