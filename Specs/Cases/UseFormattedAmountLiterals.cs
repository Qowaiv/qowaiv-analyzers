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
        _ = 12_00.Amount(); //         Noncompliant
        _ = 122_23_100.42.Amount(); // Noncompliant
    }

    void Negative()
    {
        _ = (-13.4).Amount(); // Noncompliant
        //  ^^^^^^^^^^^^^^^^ 
        _ = (Amount)(-13.4); //  Noncompliant
        //  ^^^^^^^^^^^^^^^ 
    }

    void SuffixesNotNeeded()
    {
        _ = 2.74d.Amount(); // Noncompliant
        _ = 3.14m.Amount(); // Noncompliant
    }

    void AsArgs()
    {
        Arguments(3.1.Amount()); // Noncompliant
        //        ^^^^^^^^^^^^

        Arguments((Amount)42); // Noncompliant
        //        ^^^^^^^^^^
        
        Arguments(Amount.Zero, 3.14m.Amount()); // Noncompliant
        //                     ^^^^^^^^^^^^^^
    }

    void Arguments(params Amount[] amounts) { }

    void AsIndex()
    {
        _ = this[3.14m.Amount()]; // Noncompliant
        //       ^^^^^^^^^^^^^^
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
        _ = 100.03.Amount();
        _ = 23_000.234.Amount();
        _ = (-8_413.42).Amount();
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

