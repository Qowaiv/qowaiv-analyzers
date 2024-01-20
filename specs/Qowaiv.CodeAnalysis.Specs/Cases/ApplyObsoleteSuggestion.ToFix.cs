using System;

class Applied
{
    public void StaticMethodCall()
    {
        _ = Clock.UtcNow;
    }

    public ObsoleteClass Property { }

    public ObsoleteClass ReturnType() => new();
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
