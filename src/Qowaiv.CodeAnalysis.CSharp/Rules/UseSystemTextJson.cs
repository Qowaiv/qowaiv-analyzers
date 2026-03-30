namespace Qowaiv.CodeAnalysis.Rules;

/// <summary>Implements <see cref="Rule.UseSystemTextJson"/>.</summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UseSystemTextJson() : ObsoleteTypes(
    [
        SyntaxKind.Attribute,
        SyntaxKind.FieldDeclaration,
        SyntaxKind.MethodDeclaration,
        SyntaxKind.ObjectCreationExpression,
        SyntaxKind.ParameterList,
        SyntaxKind.PropertyDeclaration,
        SyntaxKind.SimpleBaseType,
    ]
    , Rule.UseSystemTextJson)
{
    protected override void Register(AnalysisContext context)
    {
        context.RegisterSyntaxNodeAction(ReportExpressionStatement, SyntaxKind.ExpressionStatement);
        base.Register(context);
    }

    protected override void Report(SyntaxNodeAnalysisContext context, SyntaxNode node, INamedTypeSymbol type)
    {
        if (DefinedInNewtonsoft(type))
        {
            context.ReportDiagnostic(Diagnostic, node);
        }
    }

    private void ReportExpressionStatement(SyntaxNodeAnalysisContext context)
    {
        if (context.Node.Cast<ExpressionStatementSyntax>().Name()?.Contains("Newtonsoft") is true)
        {
            context.ReportDiagnostic(Diagnostic, context.Node);
        }
    }

    private static bool DefinedInNewtonsoft(INamedTypeSymbol type)
        => type.ContainingAssembly.Name == "Newtonsoft.Json";
}
