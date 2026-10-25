namespace Qowaiv.CodeAnalysis;

public static partial class Rule
{
    /// <summary>Compiler warning CS0618: Member is obsolete.</summary>
    /// <remarks>
    /// See https://learn.microsoft.com/en-us/dotnet/csharp/misc/cs0612.
    /// </remarks>
    public static readonly string CS0612 = nameof(CS0612);

    /// <summary>Compiler warning CS0618: Member is obsolete.</summary>
    /// <remarks>
    /// See https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/cs0618.
    /// </remarks>
    public static readonly string CS0618 = nameof(CS0618);

    /// <summary>Compiler error CS0619: Member is obsolete.</summary>
    /// <remarks>
    /// See https://learn.microsoft.com/en-us/dotnet/csharp/misc/cs0619.
    /// </remarks>
    public static readonly string CS0619 = nameof(CS0619);

    /// <summary>Sonar 6354: Use a testable date/time provider.</summary>
    /// <remarks>
    /// See https://rules.sonarsource.com/csharp/RSPEC-6354.
    /// </remarks>
    public static readonly string S6354 = nameof(S6354);
}
