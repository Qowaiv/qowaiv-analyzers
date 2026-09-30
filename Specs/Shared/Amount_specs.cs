using Qowaiv.CodeAnalysis.Shared;

namespace Shared.Amount_specs;

public class Helper
{
    [TestCase("3")]
    [TestCase("42")]
    [TestCase("1_123")]
    [TestCase("1_123m")]
    [TestCase("1_123.12")]
    [TestCase("1_123.123")]
    [TestCase("1_123.1234")]
    [TestCase("123")]
    [TestCase("12_123")]
    [TestCase("123_123")]
    public void IsFormatted(string token) => Amount.IsFormatted(token).Should().BeTrue();

    [TestCase(".23", "0.23")]
    [TestCase("1.234", "1.234")]
    [TestCase("_234", "234")]
    [TestCase("1_234.", "1_234.00")]
    [TestCase("1_234.3", "1_234.30")]
    [TestCase("_458.3m", "458.30m")]
    [TestCase("23453_458.30", "23_453_458.30")]
    public void Formats(string token, string format)
        => Amount.Format(token).Should().Be(format);
}
