using System;

class Rounds
{
    public const decimal PI = 3.141592653589793238462643383279502884m;

    public void Methods(decimal value)
    {
        _ = 3.1415m.Round();
        _ = 3.1415m.Round(2);
        _ = 3.1415m.Round(0, DecimalRounding.ToEven);
        _ = 3.1415m.Round(3, DecimalRounding.ToEven);
        _ = 3.1415m.Round(3, DecimalRounding.AwayFromZero);
        _ = 3.1415m.Round(3, DecimalRounding.DirectTowardsZero);
        _ = 3.1415m.Round(3, DecimalRounding.Floor);
        _ = 3.1415m.Round(3, DecimalRounding.Ceiling);

        _ = 3.1415m.Round(0, DecimalRounding.Truncate);
        _ = 3.1415m.Round(0, DecimalRounding.Floor);
        _ = 3.1415m.Round(0, DecimalRounding.Ceiling);

        _ = PI.Round(2);
        _ = value.Round(2);
        _ = Math.Round(Value());
    }
}
