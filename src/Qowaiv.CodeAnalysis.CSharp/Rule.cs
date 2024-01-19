#pragma warning disable SA1118 // Parameter should not span multiple lines.
// For readability, here it is preferred.

namespace Qowaiv.CodeAnalysis;

public static class Rule
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
        tags: ["Test"]);

    public static DiagnosticDescriptor ParseShouldNotFail => New(
        id: 0002,
        title: "Parse should not fail",
        message: "{0}",
        description:
            "Parsing string literals should not fail, as it will crash at runtime.",
        category: Category.RuntimeError,
        severity: DiagnosticSeverity.Error,
        tags: ["Error"]);

    public static DiagnosticDescriptor DecoratePureFunctions => New(
        id: 0003,
        title: "Decorate pure functions",
        message: "Decorate this method with a [Pure] or [Impure] attribute.",
        description:
            "To help the compiler and code analyzers determine the proper usage " +
            "of pure functions.",
        category: Category.Design,
        tags: ["Diagnostics", "Contracts", "Pure function"],
        isEnabled: false);

    public static DiagnosticDescriptor TrojanCharactersAreNotAllowed => New(
        id: 0004,
        title: "Characters with Trojan Horse potential are not allowed",
        message: "Trojan Horse character U+{0:X} detected.",
        description:
            "When char",
        category: Category.Security,
        tags: ["Trojan", "Unicode"]);

    public static DiagnosticDescriptor SealClasses => New(
        id: 0005,
        title: "Seal concrete classes unless designed for inheritance",
        message: "Seal this {0} or make it explicit inheritable.",
        description:
            "Inheritance is one of the pillars of Object Oriented Programming. " +
            "Designing a class to support inheritance however, is hard. As a " +
            "consequence, it is considered a bad practice to unintentionally " +
            "allowing a class to be inheritable.",
        category: Category.Design,
        tags: ["Design"]);

    public static DiagnosticDescriptor OnlyUnsealedConcreteClassesCanBeInheritable => New(
        id: 0006,
        title: "Only unsealed concrete classes should be decorated as inheritable",
        message: "Remove the [{0}] attribute.",
        description:
            "The inheritable attribute is only meant to be used on concrete classes.",
        category: Category.Design,
        tags: ["Design"]);

    public static DiagnosticDescriptor UseFileScopedNamespaceDeclarations => New(
        id: 0007,
        title: "Use file-scoped namespace declarations",
        message: "Use a file-scoped namespace declaration instead.",
        description:
            "It reduces the number of braces in a file, and also eliminates the " +
            "wasted horizontal space to the left of the class definition, since " +
            "it no longer needs to be indented.",
        category: Category.Design,
        tags: ["Design"]);

    public static DiagnosticDescriptor DefinePropertiesAsNotNullable => New(
        id: 0008,
        title: "Define properties as not-nullable for types with a defined empty state",
        message: "Define the property as not-nullable as its type has a defined empty state.",
        description:
            "Value types with an empty state do not benefit from adding nullability " +
            "to it, because the nullable that has a value can still represent an " +
            "empty state.",
        category: Category.Design,
        tags: ["Design", "SVO", "Value Type", "Value Object"]);

    public static DiagnosticDescriptor DefineEnumPropertiesAsNotNullable => New(
        id: 0009,
        title: "Define properties as not-nullable for enums with a defined none/empty value",
        message: "Define the property as not-nullable as its type has a defined none/empty value.",
        description:
            "Enums with a none value do not benefit from adding nullability " +
            "to it, because the nullable that has a value can still represent an " +
            "none/empty state.",
        category: Category.Design,
        tags: ["Design", "Enum", "Enumeration"]);

#pragma warning disable S107 // Methods should not have too many parameters
    // it calls a ctor with even more arguments.
    private static DiagnosticDescriptor New(
        int id,
        string title,
        string message,
        string description,
        string[] tags,
        Category category,
        DiagnosticSeverity severity = DiagnosticSeverity.Warning,
        bool isEnabled = true)
#pragma warning restore S107 // Methods should not have too many parameters
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
}
