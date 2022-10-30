namespace Rules.ParseShouldNotFail_specs;

public class Verify
{
    [Test]
    public void Rule()
        => new ParseShouldNotFail()
        .ForCS()
        .AddSource(@"Cases/ParseShouldNotFail.cs")
        .AddReference<Qowaiv.Percentage>()
        .Verify();
}
