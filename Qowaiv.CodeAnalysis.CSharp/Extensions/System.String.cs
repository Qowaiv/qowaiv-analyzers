namespace System;

internal static class StringExtensions
{
    extension(string str)
    {
        /// <summary>Returns true if the string match when casing is ignored.</summary>
        public bool Matches(string other) => str.Equals(other, StringComparison.OrdinalIgnoreCase);

        /// <summary>Returns true if the strings match when casing is ignored.</summary>
        public bool IsContainedBy(string other) => other.IndexOf(str, StringComparison.OrdinalIgnoreCase) != -1;
    }
}
