namespace Fixes.Seal_class;

public class Seal_class
{
    [Test]
    public void Seals_classes_and_records()
        => new SealClasses()
        .ForCS()
        .AddSource(@"Cases/SealClass.ToFix.cs")
        .ForCodeFix<SealClass>()
        .AddSource(@"Cases/SealClass.Fixed.cs")
        .Verify();
}
