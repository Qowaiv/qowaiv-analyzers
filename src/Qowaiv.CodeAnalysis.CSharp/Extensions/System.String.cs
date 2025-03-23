namespace System;

internal static class StringExtensions
{
    /// <summary>Returns true if the string match when casing is ignored.</summary>
    public static bool Matches(this string str, string other)
        => str.Equals(other, StringComparison.OrdinalIgnoreCase);

    /// <summary>Returns true if the strings match when casing is ignored.</summary>
    public static bool IsContainedBy(this string str, string other)
        => other.IndexOf(str, StringComparison.OrdinalIgnoreCase) != -1;
}
