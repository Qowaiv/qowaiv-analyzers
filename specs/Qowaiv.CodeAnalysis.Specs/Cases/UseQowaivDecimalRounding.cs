using System;

class Noncompliant
{
    public const decimal PI = 3.141592653589793238462643383279502884m;

    void Round_on_decimal()
    {
        var d0 = Math.Round(3.1415m); // Noncompliant {{Use Qowaiv decimal rounding.}}
        //       ^^^^^^^^^^^^^^^^^^^
        var d1 = Math.Round(3.1415m, 2); // Noncompliant
        var d2 = Math.Round(3.1415m, 3, MidpointRounding.ToEven); // Noncompliant
    }

    void Other_on_decimal()
    {
        var truncated = Math.Truncate(3.1415m); // Noncompliant
        var floor = Math.Floor(3.1415m); //        Noncompliant
        var ceiling = Math.Ceiling(3.1415m); //    Noncompliant
    }

    decimal With_constant() => Math.Round(PI); // Noncompliant

    decimal With_variable(decimal value) => Math.Round(value); // Noncompliant
}

class Compliant
{
    void Round_on_double()
    {
        var d0 = Math.Round(3.1415); // Compliant
        var d1 = Math.Round(3.1415, 2); // Compliant
        var d2 = Math.Round(3.1415, 3, MidpointRounding.ToEven); // Compliant
    }

    void Other_on_double()
    {
        var truncated = Math.Truncate(3.1415); // Compliant
        var floor = Math.Floor(3.1415); //        Compliant
        var ceiling = Math.Ceiling(3.1415); //    Compliant
    }

    double With_constant() => Math.Round(Math.PI); // Compliant

    double With_variable(double value) => Math.Round(value); // Compliant
}
