using System;
using System.ComponentModel;

namespace Noncompliant
{
    [Inheritable, Other] // Noncompliant {{Remove the [Inheritable] attribute.}}
//   ^^^^^^^^^^^
    public abstract class AbstractClass { }

    [Other]
    [SupportsMocking] //Noncompliant {{Remove the [SupportsMocking] attribute.}}
//   ^^^^^^^^^^^^^^^ 
    public sealed class SealedClass { }

    [Inheritable] // Noncompliant
    public abstract record AbstractRecord{ }

    [Inheritable] // Noncompliant
    public static class StaticClass { }
}

namespace Compliant
{
    [Inheritable]
    public class DecoratedClass { } // Compliant {{Decorated with Inheritable attribute.}}

    [Obsolete, Inheritable]
    public sealed class ObsoleteClass { } // Compliant {{Obsolete code is ignored.}}
}

public class InheritableAttribute : Attribute { }
public sealed class SupportsMockingAttribute : InheritableAttribute { }
public sealed class OtherAttribute : Attribute { }
