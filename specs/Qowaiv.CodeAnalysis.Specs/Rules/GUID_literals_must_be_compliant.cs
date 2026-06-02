namespace Rules.GUID_literals_must_be_compliant;

public class Verify
{
    [Test]
    public void Code() => new GuidLiterals()
        .ForCS()
        .AddSource(@"Cases/GuidLiterals.cs")
        .Verify();
}
