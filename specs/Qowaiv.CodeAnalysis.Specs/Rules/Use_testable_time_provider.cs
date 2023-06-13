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

public class Fix
{
    [Test]
    public void QW0001()
        => new UseTestableTimeProvider()
        .ForCS()
        .AddSource(@"Cases/UseTestableTimeProvider.ToFix.cs")
        .ForCodeFix<UseQowaivClock>()
        .AddSource(@"Cases/UseTestableTimeProvider.Fixed.cs")
        .Verify();
}
