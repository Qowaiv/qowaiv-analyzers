namespace Gate
{
    using Date = Qowaiv.Date;

    public class ViaAlias
    {
        public Date Property { get; set; } // Noncompliant {{Use DateOnly instead of Date.}}
        //     ^^^^
    }
}

namespace Gate.Qualified
{
    public class FullyQualified
    {
        public global::Qowaiv.Date Property { get; set; } // Noncompliant {{Use DateOnly instead of Date.}}
        //     ^^^^^^^^^^^^^^^^^^^
    }
}

namespace Gate.Global
{
    public class ViaGlobalUsingAlias
    {
        public QDate Alias { get; set; } // Noncompliant {{Use DateOnly instead of Date.}}
        //     ^^^^^
    }
}

namespace Gate.Compliant
{
    public class Compliant
    {
        public System.DateTime Today { get; set; } // Compliant {{A type of its own.}}
        public System.DateOnly DateOnly { get; set; } // Compliant {{The advised type.}}
    }
}
