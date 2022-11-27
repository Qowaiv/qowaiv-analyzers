namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UseFileScopedNamespaceDeclarations : DiagnosticAnalyzer
{
    public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = Rule.UseFileScopedNamespaceDeclarations.Array();

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

    static bool IsSingle(NamespaceDeclarationSyntax declaration)
    {
        var root = declaration.Ancestors().Last();
        var walker = new NamespaceWalker();
        walker.Visit(root);
        return walker.Declarations == 1;
    }

    sealed class NamespaceWalker : CSharpSyntaxWalker
    {
        public int Declarations { get; private set; }

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            Declarations++;
            base.VisitNamespaceDeclaration(node);
        }
    }
}
