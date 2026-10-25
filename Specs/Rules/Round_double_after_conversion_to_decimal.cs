namespace Rules.Round_double_after_conversion_to_decimal;

public class Verify
{
    [Test]
    public void Rule()
        => new RoundDoubleAfterConversionToDecimal()
        .ForCS()
        .AddSource(@"Cases/RoundDoubleAfterConversionToDecimal.cs")
        .Verify();
}
