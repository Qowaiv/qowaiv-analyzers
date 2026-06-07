using Qowaiv;
using System;
using CLOCK = Qowaiv.Clock;

class Code
{
    public void Assign()
    {
        var provider = new Tools { Assign = new FakeTimeProvider() };
    }

    public void Argument()
    {
        var provider = new Tools(new FakeTimeProvider());
        var timestamp = Tools.GetTimestamp(new FakeTimeProvider());
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
