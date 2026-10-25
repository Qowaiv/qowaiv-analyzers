namespace Rules.GUID_literals_must_be_compliant;

public class Verify_GUID
{
    [Test]
    public void Code() => new GuidLiterals()
        .ForCS()
        .AddSource(@"Cases/GuidLiterals.cs")
        .Verify();
}

public class Verify_UUID
{
    [Test]
    public void Code() => new UuidLiterals()
        .ForCS()
        .AddSource(@"Cases/UuidLiterals.cs")
        .AddReference<Qowaiv.Uuid>()
        .Verify();

    [TestCase("_1234567890-abcd-ABCD_")]
    [TestCase("Qzwerf123adfZEfsd1234-")]
    [TestCase("Qzwerf123adfZEfsd1234+")]
    [TestCase("Qzwerf123ad/fZEfsd1234")]
    [TestCase("ikgbvcx72jkofytow2v4x4muea")]
    [TestCase("IKGBVCX72JKOFYTOW2V4X4MUEA")]
    [TestCase("7c9e6679-7425-40de-944b-e07fc1f90ae7")]
    public void Is_valid(string value) => UUID.IsValid(value).Should().BeTrue();

    [TestCase("Q1234567890-abcd-ABCD", "Too short Base-64 (21)")]
    [TestCase("Q1234567890-abcd-ABCD23", "Too long Base-64 (23)")]
    [TestCase("12dm1qresn83st62reqdw7f7c", "Too short base-32 (25)")]
    [TestCase("12dm1qresn83st62reqdw7f7cAX", "Too long base-32 (27)")]
    [TestCase("Qzwerf123adfZEfsd1234$", "Invalid character")]
    public void Is_not_valid(string value, string because) => UUID.IsValid(value).Should().BeFalse(because);
}
