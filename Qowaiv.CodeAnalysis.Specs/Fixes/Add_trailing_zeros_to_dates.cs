namespace Fixes.Add_trailing_zeros_to_dates;

public class Fixes
{
    [Test]
    public void Code()
        => new UseLeadingZerosToDefineDateConstants()
        .ForCS()
        .AddSource(@"Cases/AddTrailingZerosToDates.ToFix.cs")
        .ForCodeFix<AddTrailingZerosToDates>()
        .AddSource(@"Cases/AddTrailingZerosToDates.Fixed.cs")
        .Verify();
}

