namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class SealClasses : DiagnosticAnalyzer
{
    public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => Description.SealClasses.Array();

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(Report, SyntaxKind.ClassDeclaration, SyntaxKind.RecordDeclaration);
    }

    private void Report(SyntaxNodeAnalysisContext context)
    {
        var declaration = context.Node.MethodDeclaration(context.SemanticModel);

        if (declaration.IsConcrete
            && declaration.Symbol is { } symbol
            && !symbol.IsObsolete()
            && !symbol.GetMembers().Any(IsVirtualOrProtected))
        {
            context.ReportDiagnostic(Description.SealClasses, declaration.ChildTokens().First(t => t.IsKind(SyntaxKind.IdentifierToken)));
        }
    }

    private bool IsVirtualOrProtected(ISymbol symbol)=> symbol.IsVirtual || symbol.IsProtected();
}
