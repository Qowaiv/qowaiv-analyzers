using System;

class Conversions
{
    void Casts(double value)
    {
        _ = (decimal)1.23;
        _ = (decimal)1.23d;
        _ = (decimal)1.23D;
        _ = (decimal)value;
        _ = (decimal)(value);
    }

    void Constructors(double value)
    {
        _ = new decimal(1.23);
        _ = new decimal(value);
    }

    void Implicit_constructors(double value)
    {
        decimal from_literal = new(1.23);
        decimal from_variable = new(value);
    }

    void Converts(double value)
    {
        _ = Convert.ToDecimal(1.23);
        _ = Convert.ToDecimal(value);
    }
}
