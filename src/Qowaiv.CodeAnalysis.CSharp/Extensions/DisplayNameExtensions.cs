using System.Reflection;

namespace System;

internal static class DisplayNameExtensions
{
    [Pure]
    public static string DisplayName<TEnum>(this TEnum enumValue)
        where TEnum : struct
        => DisplayName((object)enumValue);

    [Pure]
    private static string DisplayName(object enumValue)
    {
        var str = enumValue?.ToString() ?? string.Empty;
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

    [Pure]
    public override string ToString() => Display ?? string.Empty;
}
