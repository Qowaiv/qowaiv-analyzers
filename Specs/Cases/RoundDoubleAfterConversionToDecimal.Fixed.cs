using System;

class Conversions
{
    void Casts(double value)
    {
        _ = 1.23m;
        _ = 1.23m;
        _ = 1.23m;
        _ = (decimal)value;
        _ = (decimal)(value);
    }

    void Constructors(double value)
    {
        _ = 1.23m;
        _ = new decimal(value);
    }

    void Implicit_constructors(double value)
    {
        decimal from_literal = 1.23m;
        decimal from_variable = new(value);
    }

    void Converts(double value)
    {
        _ = 1.23m;
        _ = Convert.ToDecimal(value);
    }
}
