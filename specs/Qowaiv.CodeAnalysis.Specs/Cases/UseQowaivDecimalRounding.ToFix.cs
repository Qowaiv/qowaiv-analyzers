using System;

class Rounds
{
    public const decimal PI = 3.141592653589793238462643383279502884m;

    public void Methods(decimal value)
    {
        _ = Math.Round(3.1415m);
        _ = Math.Round(3.1415m, 2);
        _ = Math.Round(3.1415m, MidpointRounding.ToEven);
        _ = Math.Round(3.1415m, 3, MidpointRounding.ToEven);
        _ = Math.Round(3.1415m, 3, MidpointRounding.AwayFromZero);
        _ = Math.Round(3.1415m, 3, MidpointRounding.ToZero);
        _ = Math.Round(3.1415m, 3, MidpointRounding.ToNegativeInfinity);
        _ = Math.Round(3.1415m, 3, MidpointRounding.ToPositiveInfinity);

        _ = Math.Truncate(3.1415m);
        _ = Math.Floor(3.1415m);
        _ = Math.Ceiling(3.1415m);

        _ = Math.Round(PI, 2);
        _ = Math.Round(value, 2);
        _ = Math.Round(Value());
    }
}
