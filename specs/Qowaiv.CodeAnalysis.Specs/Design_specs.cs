using System.Reflection;

namespace Design_specs;

public class Rules
{
    [TestCaseSource(nameof(Types))]
    public void in_Qowaiv_CodeAnalysis_Rules_namespace(Type type)
        => type.Namespace.Should().Be("Qowaiv.CodeAnalysis.Rules");

    [TestCaseSource(nameof(Types))]
    public void for_CSharp(Type type)
        => type.GetCustomAttribute<DiagnosticAnalyzerAttribute>()!
        .Languages.Should().BeEquivalentTo("C#");

    private static IEnumerable<Type> Types
        => typeof(global::Qowaiv.CodeAnalysis.Rule).Assembly
        .GetTypes()
        .Where(t => !t.IsAbstract && t.IsAssignableTo(typeof(DiagnosticAnalyzer)));
}

public class CodeFixes
{
    [TestCaseSource(nameof(Types))]
    public void in_Qowaiv_CodeAnalysis_Rules_namespace(Type type)
        => type.Namespace.Should().Be("Qowaiv.CodeAnalysis.CodeFixes");

    [TestCaseSource(nameof(Types))]
    public void for_CSharp(Type type)
        => type.GetCustomAttribute<ExportCodeFixProviderAttribute>()!
        .Languages.Should().BeEquivalentTo("C#");

    private static IEnumerable<Type> Types
        => typeof(global::Qowaiv.CodeAnalysis.Rule).Assembly
        .GetTypes()
        .Where(t => t.IsAssignableTo(typeof(CodeFixProvider)));
}
