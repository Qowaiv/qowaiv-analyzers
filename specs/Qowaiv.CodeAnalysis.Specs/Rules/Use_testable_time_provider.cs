namespace Rules.Use_testable_time_provider;

public class Verify
{
    [Test]
    public void Rule()
        => new UseTestableTimeProvider()
        .ForCS()
        .AddSource(@"Cases/UseTestableTimeProvider.cs")
        .Verify();
}
