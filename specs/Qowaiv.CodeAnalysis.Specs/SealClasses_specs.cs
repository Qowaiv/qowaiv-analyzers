namespace Rules.SealClasses_specs;

public class Verify
{
    [Test]
    public void Rule()
        => new SealClasses()
        .ForCS()
        .AddSource(@"Cases/SealClasses.cs")
        .Verify();
}
