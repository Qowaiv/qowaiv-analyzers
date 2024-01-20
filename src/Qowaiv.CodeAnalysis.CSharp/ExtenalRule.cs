namespace Qowaiv.CodeAnalysis;

internal static class ExtenalRule
{
    /// <summary>Compiler warning CS0618: Member is obsolete.</summary>
    /// <remarks>
    /// See https://learn.microsoft.com/en-us/dotnet/csharp/misc/cs0618.
    /// </remarks>
    public static readonly string CS0618 = nameof(CS0618);

    /// <summary>Sonar 6354: Use a testable date/time provider.</summary>
    /// <remarks>
    /// See https://rules.sonarsource.com/csharp/RSPEC-6354.
    /// </remarks>
    public static readonly string S6354 = nameof(S6354);
}
