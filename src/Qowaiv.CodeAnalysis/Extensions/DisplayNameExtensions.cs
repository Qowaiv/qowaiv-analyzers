using System.Reflection;

namespace System;

internal static class DisplayNameExtensions
{
    public static string DisplayName<TEnum>(this TEnum enumValue)
        where TEnum : struct
        => DisplayName((object)enumValue);

    private static string DisplayName(object enumValue)
    {
        var str = enumValue?.ToString();
        var attribute = enumValue?.GetType().GetMember(str)
            .FirstOrDefault()?
            .GetCustomAttribute<DisplayAttribute>();

        return attribute is null ? str : attribute.ToString();
    }
}

[AttributeUsage(AttributeTargets.Field)]
internal sealed class DisplayAttribute : Attribute
{
    private readonly string Display;
    public DisplayAttribute(string display) => Display = display;
    public override string ToString() => Display ?? string.Empty;
}
