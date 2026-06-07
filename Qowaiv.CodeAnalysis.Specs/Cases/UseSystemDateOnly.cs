using Compliant;
using System;

namespace Noncompliant
{
    public class SomeModel
    {
        private readonly Qowaiv.Date Field; // Noncompliant {{Use DateOnly instead of Date.}}
        //               ^^^^^^^^^^^
        public Qowaiv.Date Property { get; set; } // Noncompliant

        public Qowaiv.Date? NullableProperty { get; set; } // Noncompliant
        //     ^^^^^^^^^^^^

        public Qowaiv.Date[] Properties { get; set; } // Noncompliant
        //     ^^^^^^^^^^^

        public Qowaiv.Date SomeMethod() => default; // Noncompliant
        //     ^^^^^^^^^^^

        public Qowaiv.Date? SomeNullableMethod() => default; // Noncompliant

        public void Arguments(Qowaiv.Date argument) { } // Noncompliant
        //                    ^^^^^^^^^^^
    }

    public record SomeRecord(Qowaiv.Date Property); // Noncompliant
    //                       ^^^^^^^^^^^
}

namespace Compliant
{
    public class SomeModel
    {
        private readonly DateOnly Field; // Compliant

        public DateOnly Property { get; set; } // Compliant

        public DateOnly? NullableProperty { get; set; } // Compliant

        public DateOnly SomeMethod() => default; // Compliant

        public DateOnly? SomeNullableMethod() => default; // Compliant

        public Date OtherDate() => default; // Compliant {{Type is not involved.}}

        public int OtherType() => 42; // Compliant {{Type is not involved.}}

        public DateOnly Today() => Qowaiv.Clock.Today(); // Complaint {{The usage of Qowaiv.Date is fine.}}
    }

    public record SomeRecord(DateOnly Property); // Compliant

    public struct Date { }
}
