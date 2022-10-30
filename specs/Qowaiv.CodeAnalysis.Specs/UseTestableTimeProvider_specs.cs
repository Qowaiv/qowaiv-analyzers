namespace Rules.UseTestableTimeProvider_specs;

public class Verify
{
    [Test]
    public void Rule()
        => new UseTestableTimeProvider()
        .ForCS()
        .AddSource(@"Cases/UseTestableTimeProvider.cs")
        .Verify();
}
