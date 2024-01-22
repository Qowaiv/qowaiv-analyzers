using System;

class Applied
{
    public void StaticMethodCall()
    {
        _ = Qowaiv.Clock.UtcNow();
    }

    public ReplacementClass Property { }

    public ReplacementClass ReturnType() => new();

    void Call()
    {
        var issue = new NoError();
    }
}

class NotApplied
{
    NoSuggestion NoSuggestion { get; }
    NoMessage NoMessage { get; }
}

[Obsolete("This class is obsolete. Use ReplacementClass instead.")]
class ObsoleteClass { }

class ReplacementClass { }


[Obsolete("Don't use, we have no suggestion.")]
class NoSuggestion { }

[Obsolete]
class NoMessage { }

static class Clock
{
    [Obsolete("Use Qowaiv.Clock.UtcNow() instead.")]
    public static DateTime UtcNow => DateTime.UtcNow;
}

class NoError { }

[Obsolete("Use NoError instead.", true)]
class CompilerError { }
