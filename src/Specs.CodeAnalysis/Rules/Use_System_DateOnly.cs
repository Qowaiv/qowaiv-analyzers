namespace Rules.Use_System_DateOnly;

public class Verify
{
    [Test]
    public void Rule() => new UseSystemDateOnly()
        .ForCS()
        .AddSource(@"Cases/UseSystemDateOnly.cs")
        .AddReference<Qowaiv.Date>()
        .Verify();

    [Test]
    public void Rule_without_usings() => new UseSystemDateOnly()
        .ForCS()
        .AddSources(@"Cases/UseSystemDateOnly.Gate.cs", @"Cases/UseSystemDateOnly.GlobalUsings.cs")
        .AddReference<Qowaiv.Date>()
        .Verify();
}
