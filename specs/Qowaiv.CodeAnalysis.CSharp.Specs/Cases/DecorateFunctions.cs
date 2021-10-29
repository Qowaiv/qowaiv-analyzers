using System;
using System.Diagnostics.Contracts;
using FluentAssertions;

public class Noncompliant
{
    public int PureFunction() => 42; // Noncompliant {{Decorate this method with a [Pure] or [Impure] attribute.}}
//  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
    public int PureFunction(int scale) => scale * 42; // Noncompliant

    [Other]
    public int DecoratedOtherwise() => 42; // Noncompliant@-1
}

public class Compliant
{
    [Pure]
    public int PureFunction() => 42;

    [Obsolete]
    public int Obsolete() => 0; // Compliant {{Obsolete methods are ignored.}}

    public void Void() { } // Compliant {{Void methods are impure per definition.}}

    public IDisposable Scope() => null; // Compliant {{Disposable methods are expected to be impure.}}

    public bool TryParse(string str, out object result) // Compliant {{Methods with out parameters are expected to be impure.}}
    {
        if (str is null)
        {
            result = default;
            return false;
        }
        else
        {
            result = str;
            return true;
        }
    }

    public int GuardPositive(int number) => number; // Compliant {{Guarding is expected to be impure.}}

    [Impure]
    public int ImpureMethod(int input) => 69; // Compliant {{Decorated with something derived from an ImpureAttribute.}}

    [FluenSyntax]
    public Compliant FluentSyntax() => this; // Compliant {{Decorated with something derived from an ImpureAttribute.}}

    [CustomAssertion]
    public T SomeAssertion<T>(T subject) => subject; // Compliant {{FluentAssertions custom assertions are expected to be impure.}}
}

public class Guard
{
    public T NotNull<T>(T obj) => obj; // Compliant {{Methods on a Guard class are expected to be impure.}}
}

public class OtherAttribute : Attribute { }
public class ImpureAttribute : Attribute { }
public class FluenSyntaxAttribute : ImpureAttribute { }
