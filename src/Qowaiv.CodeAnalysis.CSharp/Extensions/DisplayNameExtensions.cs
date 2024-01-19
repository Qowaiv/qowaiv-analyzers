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
        var str = enumValue.ToString();
        var attribute = enumValue.GetType().GetMember(str)
            .Single()
            .GetCustomAttribute<DisplayAttribute>();

        return attribute is null ? str : attribute.ToString();
    }
}

[AttributeUsage(AttributeTargets.Field)]
internal sealed class DisplayAttribute(string display) : Attribute
{
    private readonly string Display = display;

    [Pure]
    public override string ToString() => Display ?? string.Empty;
}
