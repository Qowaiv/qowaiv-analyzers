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
    public void Is_valid(string value) => UUID.IsValid(value).Should().BeTrue();

    [TestCase("Q1234567890-abcd-ABCD", "Too short (21)")]
    [TestCase("Q1234567890-abcd-ABCD23", "Too long (23)")]
    [TestCase("Qzwerf123adfZEfsd1234+", "Invalid character")]
    public void Is_not_valid(string value, string because) => UUID.IsValid(value).Should().BeFalse(because);
}
