using Qowaiv.Financial;

class Noncompliant
{
    void LeftIsNullable(decimal? left)
    {
        var result = left + 42m; // Noncompliant {{Value of operand is potentially null.}}
        //           ^^^^
    }

    void RightIsNullable(decimal? right)
    {
        var result = 42 + right; // Noncompliant {{Value of operand is potentially null.}}
        //                ^^^^^
    }

    void BothAreNullable(decimal? left, decimal? right)
    {
        var result = left + right; // Noncompliant {{Result of operation is potentially null.}}
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

    void WithFallBack(int? null_0, int? null_1)
    {
        int notnil = 42;
        _ = (notnil + null_0) ?? 0;
        _ = (null_0 + notnil) ?? 0;
        _ = (null_0 + null_1) ?? 0;
    }
}
