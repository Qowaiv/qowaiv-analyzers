namespace Rules.Seal_classes;

public class Verify
{
    [Test]
    public void Rule_rule_for_classes()
        => new SealClasses()
        .ForCS()
        .AddSource(@"Cases/SealClasses.cs")
        .Verify();

    [Test]
    public void Rule_for_records()
        => new SealClasses()
        .ForCS()
        .AddSource(@"Cases/SealClasses.Records.cs")
        .Verify();
}
