using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Qowaiv.CodeAnalysis.Diagnostics;
using Qowaiv.CodeAnalysis.Syntax;
using System;
using System.Collections.Immutable;

namespace Qowaiv.CodeAnalysis
{
    public partial class UseTestableTimeProvider: DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor Rule = Description.UseTestableTimeProvider;
        public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => Rule.Array();

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(Report, SyntaxKinds.IdentifierName);
        }

        private void Report(SyntaxNodeAnalysisContext context)
        {
            if (IsDateTimeProvider(context.Node.Name())
                && context.SemanticModel.GetSymbolInfo(context.Node).Symbol is IPropertySymbol property
                && property.MemberOf(SystemType.System_DateTime))
            {
                context.ReportDiagnostic(Rule, context.Node.Parent);
            }
        }

        private bool IsDateTimeProvider(string name)
           => nameof(DateTime.Now).Equals(name)
           || nameof(DateTime.UtcNow).Equals(name)
           || nameof(DateTime.Today).Equals(name);
    }
}
