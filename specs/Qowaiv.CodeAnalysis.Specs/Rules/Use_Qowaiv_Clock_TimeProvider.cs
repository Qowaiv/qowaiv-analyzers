namespace Specs.Rules.Use_Qowaiv_Clock_TimeProvider;

public class Verify
{
    [Test]
    public void CSharp() => new UseQowaivClockTimeProvider()
        .ForCS()
        .AddSource(@"Cases/UseQowaivClockTimeProvider.cs")
        .AddReference<Qowaiv.Date>()
        .AddReference<System.TimeProvider>()
        .Verify();
}
