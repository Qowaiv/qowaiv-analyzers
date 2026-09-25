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

    [TestCase("1_123")]
    [TestCase("1_123m")]
    [TestCase("1_123.12")]
    [TestCase("1_123.123")]
    [TestCase("1_123.1234")]
    [TestCase("123")]
    [TestCase("12_123")]
    [TestCase("123_123")]
    public void IsFormatted(string token) => FormatAmountLiterals.IsFormatted(token).Should().BeTrue();

    [TestCase("1234")]
    [TestCase("_234")]
    [TestCase("1_234.")]
    [TestCase("1_234.3")]
    public void IsNotFormatted(string token) => FormatAmountLiterals.IsFormatted(token).Should().BeFalse();
}
