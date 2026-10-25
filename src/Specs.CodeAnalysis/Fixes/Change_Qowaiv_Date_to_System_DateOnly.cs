namespace Fixes.Change_Qowaiv_Date_to_System_DateOnly;

public class Fixes
{
    [Test]
    public void Code() => new UseSystemDateOnly()
        .ForCS()
        .AddReference<Qowaiv.Date>()
        .AddSource(@"Cases/ChangeQowaivDateToSystemDateOnly.ToFix.cs")
        .ForCodeFix<ChangeQowaivDateToSystemDateOnly>()
        .AddSource(@"Cases/ChangeQowaivDateToSystemDateOnly.Fixed.cs")
        .Verify();
}
