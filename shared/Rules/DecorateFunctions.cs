using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Qowaiv.CodeAnalysis.Diagnostics;
using Qowaiv.CodeAnalysis.Syntax;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Qowaiv.CodeAnalysis
{
    public partial class DecorateFunctions : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor Rule = Description.DecorateFunctions;
        public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => Rule.Array();

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(Report, SyntaxKinds.MethodDecolartion);
        }

        private void Report(SyntaxNodeAnalysisContext context)
        {
            var declaration = context.Node;
            var symbol = context.SemanticModel.GetDeclaredSymbol(declaration);

            if (symbol is IMethodSymbol method
                && ReturnsResult(method.ReturnType)
                && NoGuard(method)
                && HasNoRefOutParemeter(method.Parameters)
                && NotDecorated(method.GetAttributes()))
            {
                context.ReportDiagnostic(Rule, declaration);
            }
        }

        private bool ReturnsResult(ITypeSymbol type)
            => type.IsNot(SystemType.System_Void)
            && type.IsNot(SystemType.System_Threading_Task)
            && type.IsNot(SystemType.System_IDisposable);

        private static bool HasNoRefOutParemeter(IEnumerable<IParameterSymbol> parameters)
            => parameters.All(par => par.RefKind != RefKind.Out && par.RefKind != RefKind.Ref);

        private static bool NoGuard(IMethodSymbol method)
            => !method.Name.ToUpperInvariant().Contains("GUARD")
            && method.ContainingType.Name.ToUpperInvariant() != "GUARD";

        private static bool NotDecorated(IEnumerable<AttributeData> attributes)
            => !attributes.Any(attr => Decorated(attr.AttributeClass));

        private static bool Decorated(INamedTypeSymbol attr)
            => attr.Is(SystemType.System_ObsoleteAttribute)
            || attr.Is(SystemType.System_Diagnostics_Contracts_PureAttribute)
            || attr.Is(SystemType.FluentAssertions_CustomAssertionAttribute)
            || IsImpure(attr);

        private static bool IsImpure(INamedTypeSymbol attr)
            => "IMPURE" == attr.Name?.ToUpperInvariant()
            || "IMPUREATTRIBUTE" == attr.Name?.ToUpperInvariant()
            || attr.BaseType is { } && IsImpure(attr.BaseType);

      
    }
}
