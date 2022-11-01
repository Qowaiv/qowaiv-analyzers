using System;
using System.ComponentModel;

namespace Noncompliant
{
    public class NotAbstractNotSealedClass { } // Noncompliant {{Seal this class or make it explicit inheritable.}}
    //           ^^^^^^^^^^^^^^^^^^^^^^^^^

    public partial class PartialClass { } // Noncompliant

    public partial class PartialClass { } // Noncompliant

    public class ClassWithExplicitCtor // Noncompliant
    {
        public ClassWithExplicitCtor(object arg) { }
    }

    public class NotAbstractWithProtectedBase : AbstractBase { } // Noncompliant

    public abstract class AbstractBase
    {
        protected object Property { get; set; }
    }

    public class NotAbstractWithVirtualBase : VirtualBase { } // Noncompliant

    public abstract class VirtualBase
    {
        public virtual object Property { get; set; }
    }
}

namespace Compliant
{
    public class SomeAttribute : Attribute { } // Compliant {{Are ignored.}}

    [Inheritable]
    public class WithAttribute { } // Compliant {{Decorated with Inheritable attribute.}}

    [SupportsMocking]
    public class WithDerivedAttribute { } // Compliant  {{Decorated with derived Inheritable attribute.}}

    public abstract class AbstractClass { } // Compliant {{Abstract classes should be inherited.}}

    public sealed class SealedClass { } // Compliant {{Sealed classes can not be inherited..}}

    public static class StaticClass { } // Compliant {{Static classes can not be inherited.}}

    public class WithProtectedCtor // Compliant {{Designed for inheritance.}}
    {
        protected WithProtectedCtor() { }
    }

    public class WithProtectedProperty // Compliant {{Designed for inheritance.}}
    {
        protected object Property { get; set; }
    }

    public class WithProtectedField // Compliant {{Designed for inheritance.}}
    {
        protected object field;
    }

    public class WithProtectedMethod // Compliant {{Designed for inheritance.}}
    {
        protected object Method() => new();
    }

    public class WithVirtualProperty // Compliant {{Designed for inheritance.}}
    {
        public virtual object Property { get; set; }
    }

    public class WithVirtualMethod // Compliant {{Designed for inheritance.}}
    {
        public virtual object Method() => new();
    }

    [Obsolete]
    public class ObsoleteClass { } // Compliant {{Obsolete classes are ignored.}}

    public partial class PartialSealedClass { } // Compliant

    public sealed partial class PartialSealedClass { } // Compliant


    public class InheritableAttribute : Attribute { }
    public sealed class SupportsMockingAttribute : InheritableAttribute { }
}
