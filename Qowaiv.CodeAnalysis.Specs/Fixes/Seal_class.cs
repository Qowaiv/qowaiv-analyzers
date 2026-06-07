namespace Fixes.Seal_class;

public class Fixes
{
    [Test]
    public void Code()
        => new SealClasses()
        .ForCS()
        .AddSource(@"Cases/SealClass.ToFix.cs")
        .ForCodeFix<SealClass>()
        .AddSource(@"Cases/SealClass.Fixed.cs")
        .Verify();
}
