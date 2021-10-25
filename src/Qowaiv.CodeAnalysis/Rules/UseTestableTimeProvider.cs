using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Qowaiv.CodeAnalysis.Diagnostics;
using Qowaiv.CodeAnalysis.Syntax;
using System;
using System.Collections.Immutable;

namespace Qowaiv.CodeAnalysis.Rules
{
    public abstract class UseTestableTimeProvider<TSyntaxKind> : DiagnosticAnalyzer
        where TSyntaxKind : struct
    {
        public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => Description.UseTestableTimeProvider;

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(c =>
            {
                if (IsDateTimeProvider(c.Node.Identifier().Name)
                    && c.SemanticModel.GetSymbolInfo(c.Node).Symbol is IPropertySymbol property
                    && property.IsInType(SystemType.System_DateTime))
                {
                    c.ReportDiagnostic(this, c.Node.Parent);
                }
            },
            SyntaxKinds.Of<TSyntaxKind>().IdentifierName);
        }

        private bool IsDateTimeProvider(string name)
           => nameof(DateTime.Now).Equals(name)
           || nameof(DateTime.UtcNow).Equals(name)
           || nameof(DateTime.Today).Equals(name);
    }
}
