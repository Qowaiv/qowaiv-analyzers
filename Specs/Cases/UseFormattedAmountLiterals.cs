using Qowaiv.Financial;

class Noncompliant
{
    void Unformatted()
    {
        _ = 1200.Amount(); //     Noncompliant {{Format this amount by using group separators}}
        //  ^^^^^^^^^^^^^
        _ = 100.0.Amount(); //    Noncompliant
        _ = 88.0m.Amount(); //    Noncompliant
        _ = (Amount)23_000.00; // Noncompliant
        //  ^^^^^^^^^^^^^^^^^ 
    }

    void MissFormatted()
    {
        _ = 12_00m.Amount(); //         Noncompliant
        _ = 122_23_100.42m.Amount(); // Noncompliant
    }

    void Negative()
    {
        _ = (-13.4m).Amount(); // Noncompliant
        //  ^^^^^^^^^^^^^^^^^ 
        _ = (Amount)(-13.4m); //  Noncompliant
        //  ^^^^^^^^^^^^^^^^ 
    }

    void Suffixes()
    {
        _ = 2.740.Amount(); // Noncompliant
        _ = 2.74d.Amount(); // Noncompliant
        _ = 3.14D.Amount(); // Noncompliant
    }

    void AsArgs()
    {
        Arguments(3.1.Amount()); // Noncompliant
        //        ^^^^^^^^^^^^

        Arguments((Amount)42); // Noncompliant
        //        ^^^^^^^^^^
        
        Arguments(Amount.Zero, 3.14.Amount()); // Noncompliant
        //                     ^^^^^^^^^^^^^
    }

    void Arguments(params Amount[] amounts) { }

    void AsIndex()
    {
        _ = this[3.14.Amount()]; // Noncompliant
        //       ^^^^^^^^^^^^^
        _ = this[(Amount)42]; // Noncompliant
        //       ^^^^^^^^^^
    }

    int this[Amount index] => 42;
}

public static class Compliant
{
    static void NoNumericLiteral()
    {
        _ = "42".Amount();
    }

    static void Formatted()
    {
        _ = 1_200.Amount();
        _ = 100.03m.Amount();
        _ = 23_000.234m.Amount();
        _ = (-8_413.42m).Amount();
    }

    static void NotAmounts()
    {
        _ = 100.0.Other();
        _ = (decimal)23_000.00;
    }

    static void NoLiteral(decimal amount)
    {
        _ = amount.Amount();
        _ = (Amount)amount;
    }

    public static decimal Other(this double d) => (decimal)d;

    public static decimal Amount(this string s) => 42;
}
