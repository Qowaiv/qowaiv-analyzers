namespace Fixes.Make_property_not_nullable;

public class Fixes
{
    [Test]
    public void Code()
        => new UseEmptyInsteadOfNullable()
        .ForCS()
        .AddSource(@"Cases/MakePropertyNotNullable.ToFix.cs")
        .ForCodeFix<MakePropertyNotNullable>()
        .AddSource(@"Cases/MakePropertyNotNullable.Fixed.cs")
        .Verify();
}
