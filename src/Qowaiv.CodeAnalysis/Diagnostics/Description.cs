using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Descriptors = System.Collections.Immutable.ImmutableArray<Microsoft.CodeAnalysis.DiagnosticDescriptor>;

namespace Qowaiv.CodeAnalysis.Diagnostics
{
    public static class Description
    {
        public static readonly Descriptors UseTestableTimeProvider = New(
            id: 0001, 
            title: "Use a testable Time Provider",
            message: "Use a testable (date) time provider instead.",
            description: 
                "For testability, the behavior of time providers should " +
                "be adjustable under test. DateTime.Now, DateTime.UtcNow, " +
                "DateTime.Today lack this possibility.",
            category: Category.Testabilty);

        private static Descriptors New(
            int id,
            string title,
            string message,
            string description,
            Category category,
            DiagnosticSeverity severity = DiagnosticSeverity.Warning,
            bool enabledByDefault = true)
        {
            var descriptor = new DiagnosticDescriptor(
                $"QW{id:0000}", 
                title,
                message,
                category.ToString(),
                severity,
                enabledByDefault,
                description,
                helpLinkUri: $"https://github.com/Qowaiv/qowaiv-analyzers/tree/main/rules/$QW{id:0000}.md");

            return ImmutableArray.Create(descriptor);
        }
    }
}
