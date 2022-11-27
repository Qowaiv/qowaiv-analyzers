﻿namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UseFileScopedNamespaceDeclarations : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = Rule.UseFileScopedNamespaceDeclarations.Array();

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(Report, SyntaxKind.NamespaceDeclaration);
    }

    private void Report(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is NamespaceDeclarationSyntax declaration
            && context.Compilation.LanguageVersion() >= CSharpLanguageVersion.CSharp10
            && IsSingle(declaration))
        {
            context.ReportDiagnostic(Rule.UseFileScopedNamespaceDeclarations, declaration.Name);
        }
    }

    private static bool IsSingle(NamespaceDeclarationSyntax declaration)
    {
        var counter = new DeclarationCounter();
        counter.Visit(declaration.Ancestors().Last());
        return counter.Count == 1;
    }

    private sealed class DeclarationCounter : CSharpSyntaxWalker
    {
        public int Count { get; private set; }

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            Count++;
            base.VisitNamespaceDeclaration(node);
        }
    }
}
