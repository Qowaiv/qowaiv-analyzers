using Qowaiv;
using System;
using CLOCK = Qowaiv.Clock;

class Compliant
{
    public void Assign()
    {
        var tools = new Tools { Assign = Clock.TimeProvider };
    }

    public void Argument()
    {
        var provider = new Tools(Clock.TimeProvider);
        var timestamp = Tools.GetTimestamp(Clock.TimeProvider);
    }

    public void Alias()
    {
        var provider = new Tools { Assign = CLOCK.TimeProvider };
    }

    public void Ignore()
    {
        var otherType = new Guid("64365EDA-7FFD-46FC-BC10-8922A6507AF3"); // Compliant, other type.
    }
}

class Noncompliant
{
    public void Assign()
    {
        var provider = new Tools { Assign = new FakeTimeProvider() }; // Noncompliant {{Use Qowaiv.Clock.TimeProvider}}
        //                                  ^^^^^^^^^^^^^^^^^^^^^^
    }

    public void Argument()
    {
        var provider = new Tools(new FakeTimeProvider()); // Noncompliant
        //                       ^^^^^^^^^^^^^^^^^^^^^^
        var timestamp = Tools.GetTimestamp(new FakeTimeProvider()); // Noncompliant
    }

    public void MemberCall()
    {
        var provider = TimeProvider.System;
    }
}

public class Tools(TimeProvider provider)
{
    public Tools() : this(Clock.TimeProvider) { }

    public static long GetTimestamp(TimeProvider tp) => tp.GetTimestamp();

    public TimeProvider Assign { get; init; }
}

public class FakeTimeProvider : System.TimeProvider
{
    public override long GetTimestamp() => 0;
}
