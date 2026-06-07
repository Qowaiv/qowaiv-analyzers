namespace Rules.Use_System_DateOnly;

public class Verify
{
    [Test]
    public void Rule() => new UseSystemDateOnly()
        .ForCS()
        .AddSource(@"Cases/UseSystemDateOnly.cs")
        .AddReference<Qowaiv.Date>()
        .Verify();
}
