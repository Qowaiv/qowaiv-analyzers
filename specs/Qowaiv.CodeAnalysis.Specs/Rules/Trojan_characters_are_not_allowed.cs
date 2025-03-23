namespace Rules.Trojan_characters_are_not_allowed;

public class Verify
{
    [Test]
    public void Rule()
        => new TrojanCharactersAreNotAllowed()
        .ForCS()
        .AddSource(@"Cases/TrojanCharactersAreNotAllowed.cs")
        .Verify();

    [TestCase(0x1D453, "𝑓 symbol")]
    public void NoVulnerability(int utf32, string message)
        => TrojanCharactersAreNotAllowed.IsVulnerability(utf32).Should().BeFalse(because: message);

    [TestCase('\t', "horizontal tab")]
    public void NoVulnerability(char ch, string message)
         => NoVulnerability(ch, ch, message);

    [TestCase(0x00020, 0x0007E, "ASCII - space ... ~")]
    [TestCase(0x00600, 0x006FF, "Arabic")]
    [TestCase(0x00750, 0x0077F, "Arabic Supplement")]
    [TestCase(0x00870, 0x0089F, "Arabic Extended-B")]
    [TestCase(0x008A0, 0x008FF, "Arabic Extended-A")]
    [TestCase(0x0FB50, 0x0FDFF, "Arabic Presentation Forms-A")]
    [TestCase(0x0FE70, 0x0FEFF, "Arabic Presentation Forms-B")]
    [TestCase(0x10E60, 0x10E7F, "Rumi Numeral Symbols")]
    [TestCase(0x1EC70, 0x1ECBF, "Indic Siyaq Numbers")]
    [TestCase(0x1ED00, 0x1ED4F, "Ottoman Siyaq Numbers")]
    [TestCase(0x1EE00, 0x1EEFF, "Arabic Mathematical Alphabetic Symbols")]
    [TestCase(0x04E00, 0X09FEF, "CJK Unified Ideographs")]
    [TestCase(0x03400, 0X04DBF, "CJK Unified Ideographs Extension A")]
    [TestCase(0x20000, 0X2A6DF, "CJK Unified Ideographs Extension B")]
    [TestCase(0x2A700, 0X2B73F, "CJK Unified Ideographs Extension C")]
    [TestCase(0x2B740, 0X2B81F, "CJK Unified Ideographs Extension D")]
    [TestCase(0x2B820, 0X2CEAF, "CJK Unified Ideographs Extension E")]
    [TestCase(0x2CEB0, 0X2EBEF, "CJK Unified Ideographs Extension F")]
    [TestCase(0x03007, 0X03007, "block CJK Symbols and Punctuation")]
    [TestCase(0x00400, 0x004FF, "Cyrillic")]
    [TestCase(0x00500, 0x0052F, "Cyrillic Supplement")]
    [TestCase(0x02DE0, 0x02DFF, "Cyrillic Extended-A")]
    [TestCase(0x0A640, 0x0A69F, "Cyrillic Extended-B")]
    [TestCase(0x01C80, 0x01C8F, "Cyrillic Extended-C")]
    [TestCase(0x01D2B, 0x01D78, "Phonetic Extensions")]
    [TestCase(0x0FE2E, 0x0FE2F, "Combining Half Marks")]
    public void NoVulnerability(int start, int end, string range)
    {
        var utf32s = Enumerable.Range(start, 1 + end - start);
        var vulnerabilities = utf32s.Where(utf32 => TrojanCharactersAreNotAllowed.IsVulnerability(utf32));
        vulnerabilities.Should().BeEmpty(because: "U+{0:X}-U+{1:X} represents {2}.", start, end, range);
    }
}
