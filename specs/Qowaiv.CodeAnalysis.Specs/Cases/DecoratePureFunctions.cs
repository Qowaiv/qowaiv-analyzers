using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using FluentAssertions;

public class Noncompliant
{
    public int PureFunction() => 42; // Noncompliant {{Decorate this method with a [Pure] or [Impure] attribute.}}
    //         ^^^^^^^^^^^^
    
    public int PureFunction(int scale) => scale * 42; // Noncompliant

    [Other]
    public int DecoratedOtherwise() => 42; // Noncompliant
}

public class Compliant
{
    [Pure]
    public int PureFunction() => 42;

    [Obsolete]
    public int Obsolete() => 0; // Compliant {{Obsolete methods are ignored.}}

    [Impure]
    public int ImpureMethod(int input) => 42; // Compliant {{Decorated with something derived from an ImpureAttribute.}}

    [FluentSyntax]
    public Compliant FluentSyntax() => this; // Compliant {{Decorated with something derived from an ImpureAttribute.}}

    [CustomAssertion]
    public T SomeAssertion<T>(T subject) => subject; // Compliant {{FluentAssertions custom assertions are expected to be impure.}}

    [System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute()]
    public int WhenItNeverReturns() => throw new NotSupportedException(); // Compliant {{DoesNotReturn also indicates cleary what to expect.}}
}

public ref struct ComplaintStruct
{
    public readonly int Result() => 42; // Compliant {{The readonly modifier should be sufficient.}}
}

public class ImpureByAssumption
{
    public void Void() { } // Compliant {{Void methods are impure per definition.}}

    public Task AsyncVoid() => Task.CompletedTask; // Compliant {{Async void methods are impure per definition.}}

    public async ValueTask AsyncVoidStruct() => await AsyncVoid(); // Compliant {{Async void methods are impure per definition.}}

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
}

public class Guard
{
    public T NotNull<T>(T obj) => obj; // Compliant {{Methods on a Guard class are expected to be impure.}}
}

[Obsolete]
public class ObsoleteClass
{
    public int Function() => 42; // Compliant {{Obsolete classes are ignored.}}
}

public class OtherAttribute : Attribute { }
public class ImpureAttribute : Attribute { }
public class FluentSyntaxAttribute : ImpureAttribute { }
