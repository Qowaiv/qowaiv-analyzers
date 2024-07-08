namespace Rules.Use_Qowaiv_Decimal_Rounding;

public class Verify
{
    [Test]
    public void Rule() => new UseQowaivDecimalRounding()
        .ForCS()
        .AddSource(@"Cases/UseQowaivDecimalRounding.cs")
        .AddReference<Qowaiv.DecimalRounding>()
        .Verify();
}
