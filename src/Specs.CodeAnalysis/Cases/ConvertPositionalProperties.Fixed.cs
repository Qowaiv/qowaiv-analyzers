using System;

public record WithoutBody()
{
    public required string Message { get; init; }
}

public record WithBody()
{
    public string Trimmed => Message.Trim();

    public required string Message { get; init; }
}

public record WithDefault()
{
    public required string Message { get; init; } = "default";
}

public record WithOptional()
{
    public string? Message { get; init; }
}

public record WithMultiple()
{
    public int? Value0 { get; init; }

    public required int Value1 { get; init; }

    public required string Value2 { get; init; }

    public string? Value3 { get; init; } = "help";
}

public sealed record WithAttribute()
{
    [Prop]
    public required object First { get; init; }

    [Prop]
    public required string Second { get; init; }
}

public sealed record ChildRecord(string Message) : BaseRecord(Message)
{
    public string? Extra { get; init; }
}

[Obsolete]
public abstract record BaseRecord(string Message);

[AttributeUsage(AttributeTargets.Property)]
public sealed class PropAttribute : Attribute { }
