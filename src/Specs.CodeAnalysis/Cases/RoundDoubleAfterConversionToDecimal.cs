using System;

class Noncompliant
{
    void Casts(double value)
    {
        _ = (decimal)value; // Noncompliant {{Round this converted result}}
        //  ^^^^^^^^^^^^^^
        _ = (decimal)1.23; // Noncompliant
        _ = (decimal)(value); // Noncompliant
    }

    void Casts_to_nullable(double value)
    {
        _ = (decimal?)value; // Noncompliant
    }

    void Casts_of_nullable(double? value)
    {
        _ = (decimal)value; // Noncompliant
    }

    void Constructors(double value)
    {
        _ = new decimal(value); // Noncompliant
        //  ^^^^^^^^^^^^^^^^^^
        _ = new decimal(1.23); // Noncompliant
    }

    void Implicit_constructors(double value)
    {
        decimal from_variable = new(value); // Noncompliant
        decimal from_literal = new(1.23); // Noncompliant
    }

    void Converts(double value)
    {
        _ = Convert.ToDecimal(value); // Noncompliant
        //  ^^^^^^^^^^^^^^^^^^^^^^^^
        _ = Convert.ToDecimal(1.23); // Noncompliant
    }

    void Rounded_before(double value)
    {
        _ = (decimal)Math.Round(value); // Noncompliant
        _ = (decimal)Math.Round(value, 2); // Noncompliant
        _ = (decimal)Math.Round(value, 2, MidpointRounding.AwayFromZero); // Noncompliant
        _ = (decimal)(Math.Round(value, 2)); // Noncompliant
        _ = new decimal(Math.Round(value, 2)); // Noncompliant
        _ = Convert.ToDecimal(Math.Round(value, 2)); // Noncompliant
    }
}

class Compliant
{
    void Rounded_after(double value)
    {
        _ = Math.Round((decimal)value, 2); // Compliant
        _ = decimal.Round((decimal)value, 2); // Compliant
        _ = Math.Round((decimal)1.23, 2); // Compliant
        _ = Math.Round((decimal)Math.Round(value, 1), 2); // Compliant
    }

    void Other_conversions(int integer, float single, decimal dec, object boxed)
    {
        _ = (decimal)integer; // Compliant
        _ = (decimal)single; // Compliant
        _ = (decimal)dec; // Compliant
        _ = (decimal)boxed; // Compliant
        _ = new decimal(integer); // Compliant
        _ = new decimal(single); // Compliant
        _ = Convert.ToDecimal(integer); // Compliant
        _ = Convert.ToDecimal(single); // Compliant
        _ = (double)dec; // Compliant
    }
}
