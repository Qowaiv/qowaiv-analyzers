namespace Rules.Use_leading_zeros_to_define_date_constants;

public class Verify
{
    [Test]
    public void Rule() => new UseLeadingZerosToDefineDateConstants()
        .ForCS()
        .AddSource(@"Cases/UseLeadingZerosToDefineDateConstants.cs")
        .AddReference<Qowaiv.Date>()
        .Verify();
}
