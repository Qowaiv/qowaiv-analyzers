using Qowaiv;

namespace Fixes.Use_Qowaiv_Round_Extensions;

public class Fixes
{
    [Test]
    public void Code()
        => new UseQowaivDecimalRounding()
        .ForCS()
        .AddSource(@"Cases/UseQowaivDecimalRounding.ToFix.cs")
        .ForCodeFix<UseQowaivRoundExtensions>()
        .AddSource(@"Cases/UseQowaivDecimalRounding.Fixed.cs")
        .Verify();
}

public class Rounding
{
    [TestCase(MidpointRounding.ToEven, DecimalRounding.ToEven)]
    [TestCase(MidpointRounding.ToZero, DecimalRounding.DirectTowardsZero)]
    [TestCase(MidpointRounding.AwayFromZero, DecimalRounding.AwayFromZero)]
    [TestCase(MidpointRounding.ToPositiveInfinity, DecimalRounding.Ceiling)]
    [TestCase(MidpointRounding.ToNegativeInfinity, DecimalRounding.Floor)]
    public void Are_equivalent(MidpointRounding midpointRounding, DecimalRounding decimalRounding)
    {
        numbers.Should().AllSatisfy(n => 
            n.Round(0, decimalRounding).Should().Be(Math.Round(n, midpointRounding), because: $"{n}"));
    }

    public static readonly decimal[] numbers =
    [
        -18.0m, -18.1m, -18.2m, -18.3m, -18.4m, -18.5m, -18.6m, - 18.8m, -18.9m,
        -17.0m, -17.1m, -17.2m, -17.3m, -17.4m, -17.5m, -17.6m, - 17.8m, -17.9m,
        +17.0m, +17.1m, +17.2m, +17.3m, +17.4m, +17.5m, +17.6m, + 17.8m, +17.9m,
        +18.0m, +18.1m, +18.2m, +18.3m, +18.4m, +18.5m, +18.6m, + 18.8m, +18.9m,
    ];
}

