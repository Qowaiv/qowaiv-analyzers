using System;

public record WithoutBody(string Message);

public record WithBody(string Message)
{
    public string Trimmed => Message.Trim();
}

public record WithDefault(string Message = "default");

public record WithOptional(string? Message);

public record WithMultiple(int? Value0, int Value1, string Value2, string? Value3 = "help");

public sealed record WithAttribute([Prop] object First, [property: Prop] string Second);

public sealed record ChildRecord(string? Extra, string Message) : BaseRecord(Message);

[Obsolete]
public abstract record BaseRecord(string Message);

[AttributeUsage(AttributeTargets.Property)]
public sealed class PropAttribute : Attribute { }
