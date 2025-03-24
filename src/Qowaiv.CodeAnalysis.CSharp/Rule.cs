#pragma warning disable SA1118 // Parameter should not span multiple lines.
// For readability, here it is preferred.

using static Qowaiv.CodeAnalysis.Syntax.TypeDeclaration;

namespace Qowaiv.CodeAnalysis;

public static partial class Rule
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
            "When characters with Trojan Horse potential are found, they should be blocked " +
            "to avoid security risks to the application.",
        category: Category.Security,
        tags: ["Trojan", "Unicode"]);

    public static DiagnosticDescriptor SealClasses => New(
        id: 0005,
        title: "Seal concrete classes unless designed for inheritance",
        message: "Seal this {0} or make it explicitly inheritable.",
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

    public static DiagnosticDescriptor UseSystemDateOnly => New(
       id: 0010,
       title: "Use System.DateOnly instead of Qowaiv.Date",
       message: "Use DateOnly instead of Date.",
       description:
            "The purpose of `Qowaiv.Date` is to provide a date (only) alternative to DateTime. " +
            "Since .NET 6.0, Microsoft provides DateOnly.",
       category: Category.Design,
       tags: ["Design", "SVO"]);

    public static DiagnosticDescriptor DefinePropertiesAsImmutables => New(
        id: 0011,
        title: "Define properties as immutables",
        message: "Remove this setter, make this property init-only, or decorate the type with the mutable attribute.",
        description:
            "Immutable types (classes, interfaces, records, structs) have multiple advantages. " +
            "To benefit from this, all properties should be defined as immutable.",
        category: Category.Design,
        tags: ["Design", "Immutability"]);

    public static DiagnosticDescriptor UseImmutableTypesForProperties => New(
        id: 0012,
        title: "Use immutable types for properties",
        message: "Use an immutable type, or decorate the type with the mutable attribute.",
        description:
            "Immutable types (classes, interfaces, records, structs) have multiple advantages. " +
            "To benefit from this, the type of properties should be immutable.",
        category: Category.Design,
        tags: ["Design", "Immutability"]);

    public static DiagnosticDescriptor UseQowaivDecimalRounding => New(
        id: 0013,
        title: "Use Qowaiv decimal rounding",
        message: "Use Qowaiv decimal rounding.",
        description:
            "Both the extended functionality and the extension method are reasons to adopt Qowaiv decimal rounding.",
        category: Category.Design,
        tags: ["Design", "Readability"]);

    public static DiagnosticDescriptor DefineGlobalUsingStatementsSeparately => New(
        id: 0014,
        title: "Define global using statements separately",
        message: "Define global using statement in a separate file.",
        description:
            "For design and maintainability reasons, it is key that all global usings statements are grouped.",
        category: Category.Design,
        tags: ["Design", "Maintainability"]);

    public static DiagnosticDescriptor DefineGlobalUsingStatementsInSingleFile => New(
        id: 0015,
        title: "Define global using statements in single file",
        message: "Define global using statements in '{0}' only.",
        description:
            "For design and maintainability reasons, it is key that all global usings statements are grouped.",
        category: Category.Design,
        tags: ["Design", "Maintainability"]);

    public static DiagnosticDescriptor PreferRegularOverPositionalProperties => New(
        id: 0016,
        title: "Prefer regular over positional properties",
        message: "Define {0} as {1}.",
        description:
            "The usage of positional properties, defined in a primary constructor, " +
            "turns out to be cumbersome for public APIs. Therefor the use of " +
            "regular properties is preferred in those cases.",
        category: Category.Design,
        tags: ["Design", "Maintainability"]);

    public static DiagnosticDescriptor ApplyArithmeticOperationsOnNonNullablesOnly => New(
        id: 0017,
        title: "Apply arithmetic operations on non-nullables only",
        message: "{0} is potentially null.",
        description:
            ".NET allows arithmetic operations between nullable value types. The " +
            "outcome will be null if any of the arguments turns out to be null, " +
            "which can be confusing or a bug.",
        category: Category.Bug,
        tags: ["nullability", "arithmetic"]);

    public static DiagnosticDescriptor DefineOnlyOneRequiredAttribute => New(
        id: 0100,
        title: "Define only one Required attribute",
        message: "{0} should not be decorated with more than one required attribute",
        description:
            "The compiler cannot enforce single usages for overridden implementations " +
            "of the [Required] attribute, but would otherwise disallow it.",
        category: Category.Bug,
        tags: ["Data Annotations", "AttributeUsage", "Validation", "RequiredAttribute"]);

    public static DiagnosticDescriptor RequiredCannotInvalidateValueTypes => New(
        id: 0101,
        title: "Required attribute cannot invalidate value types",
        message: "The value of this value type will always meet the Required constraints",
        description:
            "The implementation of the Required attribute is to check if the " +
            "value is not null. This is always true for non-nullable value types.",
        category: Category.Bug,
        tags: ["Data Annotations", "Validation", "RequiredAttribute"]);

    public static DiagnosticDescriptor UseCompliantValdationAttribute => New(
        id: 0102,
        title: "Use compliant validation attributes",
        message: "The attribute cannot validate this type",
        description:
            "Validation attributes are designed to validate certain member types. " +
            "When applied on other types, this is invalid, and might even crash.",
        category: Category.Bug,
        tags: ["Data Annotations", "Validation", "ValidationAttribute"]);

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
