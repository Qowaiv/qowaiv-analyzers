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
    [TestCase("000_123")]
    public void IsFormatted(string token) => Amount.IsFormatted(token).Should().BeTrue();

    [TestCase("23d", "23")]
    [TestCase("23.4D", "23.40")]
    [TestCase("1_458.3m", "1_458.30")]
    [TestCase("1_458.3M", "1_458.30")]
    [TestCase(".23", "0.23")]
    [TestCase("1.234", "1.234")]
    [TestCase("1_234.", "1_234.00")]
    [TestCase("1_234.3", "1_234.30")]
    [TestCase("_458.3m", "458.30m")]
    [TestCase("23453_458.30", "23_453_458.30")]
    [TestCase("17E-2", "0.17")]
    [TestCase("3.14e3", "3_140")]
    [TestCase("0b111", "7")]
    [TestCase("0xFF", "255")]
    public void Formats(string token, string format)
        => Amount.Format(token).Should().Be(format);
}
