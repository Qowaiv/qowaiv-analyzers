using Qowaiv.Financial;

class Noncompliant
{
    void LeftIsNullable(decimal? left)
    {
        var result = left + 42m; // Noncompliant
        //           ^^^^
    }

    void RightIsNullable(decimal? right)
    {
        var result = 42 + right; // Noncompliant
        //                ^^^^^
    }

    void BothAreNullable(decimal? left, decimal? right)
    {
        var result = left + right; // Noncompliant
        //           ^^^^^^^^^^^^
    }

    void AllOperators(decimal? left)
    {
        var add = left + 42; //       Noncompliant
        var subtract = left - 42; //  Noncompliant
        var multiply = left * 42; //  Noncompliant
        var divide = left / 42; //    Noncompliant
        var remainder = left % 42; // Noncompliant
    }

    void OtherTypes()
    {
        var amount = 42.Amount() + default(Amount?); // Noncompliant;
        var dlb = 42.0 + default(double?); //           Noncompliant;
    }

    void Assignment(decimal? value)
    {
        decimal? x = 42;
        x += value; // Noncompliant
//      ^^^^^^^^^^
        x -= value; // Noncompliant
        x *= value; // Noncompliant
        x /= value; // Noncompliant
        x %= value; // Noncompliant
    }

    void Assignment(decimal? init, decimal value)
    {
        init += value; // Noncompliant
//      ^^^^
    }
}

class Compliant
{
    void BothNotNullable(decimal left, decimal right)
    {
        var result = left + right;
    }

    void AllOperators(decimal left)
    {
        var add = left + 42;
        var subtract = left - 42;
        var multiply = left * 42;
        var divide = left / 42;
        var remainder = left % 42;
    }

    void OtherTypes()
    {
        var amount = 42.Amount() + 3.14.Amount();
        var dlb = 42.0 + System.Math.PI;
    }

    void Assignment(decimal value)
    {
        decimal x = 42;
        x += value;
        x -= value;
        x *= value;
        x /= value;
        x %= value;
    }
}
