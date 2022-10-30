namespace Qowaiv.CodeAnalysis.Diagnostics;

public static class Description
{
    public static DiagnosticDescriptor UseTestableTimeProvider => New(
        id: 0001,
        title: "Use a testable Time Provider",
        message: "Use a testable (date) time provider instead.",
        description:
            "For testability, the behavior of time providers should " +
            "be adjustable under test. DateTime.Now, DateTime.UtcNow, " +
            "and DateTime.Today lack this possibility.",
        category: Category.Testabilty,
        tags: new[] { "Test" });

    public static DiagnosticDescriptor ParseShouldNotFail => New(
        id: 0002,
        title: "Parse should not fail",
        message: "{0}",
        description:
            "Parsing string literals should not fail, as it will crash at runtime.",
        category: Category.RuntimeError,
        severity: DiagnosticSeverity.Error,
        tags: new[] { "Error" });

    public static DiagnosticDescriptor DecorateFunctions => New(
        id: 0003,
        title: "Decorate pure functions",
        message: "Decorate this method with a [Pure] or [Impure] attribute.",
        description:
            "To help the compiler and code analyzers determine the proper usage " +
            "of pure functions.",
        category: Category.Design,
        tags: new[] { "Diagnostics", "Contracts", "Pure function" },
        isEnabled: false);

    public static DiagnosticDescriptor TrojanCharactersAreNotAllowed => New(
        id: 0004,
        title: "Characters with Trojan Horse potential are not allowed",
        message: "Trojan Horse character U+{0:X} detected.",
        description:
            "When char",
        category: Category.Security,
        tags: new[] { "Trojan", "Unicode", "" });

    public static DiagnosticDescriptor SealClasses => New(
        id: 0005,
        title: "Seal concrete classes unless designed for inheritance",
        message: "Seal this class.",
        description:
            "Inheritance is a powerful thing in Object Oriented Programming. " +
            "That being said, it should be cautious decision to allow " +
            "inheritance on a class, as design for inheritance is hard.",
       category: Category.Design,
       tags: new[] { "Design" });

    private static DiagnosticDescriptor New(
        int id,
        string title,
        string message,
        string description,
        string[] tags,
        Category category,
        DiagnosticSeverity severity = DiagnosticSeverity.Warning,
        bool isEnabled = true)
        => new(
            id: $"QW{id:0000}",
            title: title,
            messageFormat: message,
            customTags: tags,
            category: category.DisplayName(),
            defaultSeverity: severity,
            isEnabledByDefault: isEnabled,
            description: description,
            helpLinkUri: $"https://github.com/Qowaiv/qowaiv-analyzers/blob/main/rules/QW{id:0000}.md");

    public static ImmutableArray<DiagnosticDescriptor> Array(this DiagnosticDescriptor descriptor)
        => ImmutableArray.Create(descriptor);
}
