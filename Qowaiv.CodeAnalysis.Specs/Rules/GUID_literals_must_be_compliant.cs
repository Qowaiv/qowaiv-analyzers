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
}
