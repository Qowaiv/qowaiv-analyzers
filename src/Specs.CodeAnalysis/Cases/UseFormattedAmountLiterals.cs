using Qowaiv.Financial;
using System;

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

    [System.Diagnostics.Contracts.Pure]
    void DecoratedOtherwise() => _ = 1200.Amount(); // Noncompliant {{Format this amount by using group separators}}

    [JustDecorated]
    static Amount DecoratedProperty { get; set; } = 100.0.Amount(); // Noncompliant {{Format this amount by using group separators}}

    [JustDecorated]
    static readonly Amount DecoratedField = 100.0.Amount(); // Noncompliant {{Format this amount by using group separators}}

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

    [Obsolete]
    static void ObsoleteMethod()
    {
        _ = 1200.Amount(); // Compliant {{Obsolete methods are ignored.}}
    }

    [Obsolete]
    static void WithLambda()
    {
        Func<Amount> lambda = () => 100.0.Amount(); // Compliant {{Obsolete methods are ignored.}}
    }

    [Obsolete]
    static void WithLocalFunction()
    {
        Amount Local() => 100.0.Amount(); // Compliant {{Obsolete methods are ignored.}}
        _ = Local();
    }

    [Obsolete]
    static readonly Amount ObsoleteField = 100.0.Amount(); // Compliant {{Obsolete fields are ignored.}}

    [Obsolete]
    static Amount ObsoleteProperty { get; set; } = 100.0.Amount(); // Compliant {{Obsolete properties are ignored.}}

    [Obsolete("Will be dropped when the next major version is released.")]
    public sealed class Will_be_dropped
    {
        public void double_Amount_humanizer()
            => _ = 12.00.Amount().Equals(0.Amount()); // Compliant {{Obsolete classes are ignored.}}
    }

    [Obsolete]
    public class ObsoleteOuter
    {
        public class Nested
        {
            public void Unformatted() => _ = 88.0m.Amount(); // Compliant {{Obsolete classes are ignored.}}
        }
    }

    public static decimal Other(this double d) => (decimal)d;

    public static decimal Amount(this string s) => 42;
}

[Obsolete]
class ObsoleteClassWithProperty
{
    Amount Property { get; set; } = 100.0.Amount(); // Compliant {{Obsolete classes are ignored.}}
}

partial class WithPartialMethod
{
    [Obsolete]
    partial void Method();
}

partial class WithPartialMethod
{
    partial void Method()
    {
        _ = 1200.Amount(); // Compliant {{Obsolete methods are ignored.}}
    }
}

class WithObsoleteConstructorAndOperator
{
    [Obsolete]
    WithObsoleteConstructorAndOperator()
    {
        _ = 1200.Amount(); // Compliant {{Obsolete methods are ignored.}}
    }

    [Obsolete]
    public static WithObsoleteConstructorAndOperator operator +(
        WithObsoleteConstructorAndOperator left,
        WithObsoleteConstructorAndOperator right)
    {
        _ = 1200.Amount(); // Compliant {{Obsolete methods are ignored.}}
        return left;
    }
}

class JustDecorated : Attribute { }

[JustDecorated]
class DecoratedClass
{
    void Unformatted() => _ = 1200.Amount(); // Noncompliant {{Format this amount by using group separators}}
}
