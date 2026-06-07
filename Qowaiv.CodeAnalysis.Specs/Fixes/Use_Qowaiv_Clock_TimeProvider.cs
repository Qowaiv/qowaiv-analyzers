namespace Fixes.Use_Qowaiv_Clock_TimeProvider;

public class Fixes
{
    [Test]
    public void Code() => new UseQowaivClockTimeProvider()
        .ForCS()
        .AddSource(@"Cases/UseQowaivClockTimeProvider.ToFix.cs")
        .ForCodeFix<ChangeToQowaivClockTimeProvider>()
        .AddSource(@"Cases/UseQowaivClockTimeProvider.Fixed.cs")
        .Verify();
}
