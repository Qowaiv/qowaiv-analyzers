using System;
using System.ComponentModel;

namespace Noncompliant
{
    public record NotAbstractNotSealedRecord { } // Noncompliant {{Seal this record.}}
    //            ^^^^^^^^^^^^^^^^^^^^^^^^^^

    public partial record PartialRecord { } // Noncompliant

    public partial record PartialRecord { } // Noncompliant

    public record RecordWithExplicitCtor // Noncompliant
    {
        public RecordWithExplicitCtor(object arg) { }
    }

    public record NotAbstractWithProtectedBase : AbstractBase { } // Noncompliant

    public abstract record AbstractBase
    {
        protected object Property { get; set; }
    }

    public record NotAbstractWithVirtualBase : VirutalBase { } // Noncompliant

    public abstract record VirutalBase
    {
        public virtual object Property { get; set; }
    }
}

namespace Compliant
{
    [Inheritable]
    public record WithAttribute { } // Compliant {{Decorated with Inheritable attribute.}}

    [SupportsMocking]
    public record WithDerivedAttribute { } // Compliant  {{Decorated with derived Inheritable attribute.}}

    public abstract record AbstractRecord { } // Compliant {{Abstract.}}

    public sealed record SealedRecord { } // Compliant {{Sealed.}}

    public record WithProtectedCtor // Compliant {{Designed for inheritance.}}
    {
        protected WithProtectedCtor() { }
    }

    public record WithProtectedProperty // Compliant {{Designed for inheritance.}}
    {
        protected object Property { get; set; }
    }

    public record WithProtectedField // Compliant {{Designed for inheritance.}}
    {
        protected object field;
    }

    public record WithProtectedMethod // Compliant {{Designed for inheritance.}}
    {
        protected object Method() => new();
    }

    public record WithVirtualProperty // Compliant {{Designed for inheritance.}}
    {
        public virtual object Property { get; set; }
    }

    public record WithVirtualMethod // Compliant {{Designed for inheritance.}}
    {
        public virtual object Method() => new();
    }

    [Obsolete]
    public record ObsoleteRecord { } // Compliant {{Obsolete classes are ignored.}}

    public partial record PartialSealedRecord { } // Compliant

    public sealed partial record PartialSealedRecord { } // Compliant


    public class InheritableAttribute : Attribute { }
    public sealed class SupportsMockingAttribute : InheritableAttribute { }
}
