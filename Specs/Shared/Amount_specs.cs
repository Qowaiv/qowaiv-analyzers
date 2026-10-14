using Qowaiv.CodeAnalysis.Shared;

namespace Shared.Amount_specs;

public class Helper
{
    [TestCase("3")]
    [TestCase("42")]
    [TestCase("1_123")]
    [TestCase("1_123m")]
    [TestCase("1_123.12m")]
    [TestCase("1_123.123m")]
    [TestCase("1_123.1234m")]
    [TestCase("123")]
    [TestCase("12_123")]
    [TestCase("123_123")]
    [TestCase("000_123")]
    [TestCase("1_458.30m")]
    [TestCase("1_458.30M")]
    public void IsFormatted(string token) => Amount.IsFormatted(token).Should().BeTrue();

    [TestCase("2.740", "2.740m")]
    [TestCase("23.4D", "23.40m")]
    [TestCase(".23", "0.23m")]
    [TestCase(".23m", "0.23m")]
    [TestCase("1.234", "1.234m")]
    [TestCase("1_234.3m", "1_234.30m")]
    [TestCase("_458.3m", "458.30m")]
    [TestCase("23453_458.30m", "23_453_458.30m")]
    [TestCase("17E-2", "0.17m")]
    [TestCase("3.14e3", "3_140")]
    [TestCase("0b111", "7")]
    [TestCase("0xFF", "255")]
    public void Formats(string token, string format)
        => Amount.Format(token).Should().Be(format);
}
