namespace Qowaiv.CodeAnalysis.Rules;

/// <summary>Implements <see cref="Rule.UseSystemDateOnly"/>.</summary>
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
    protected override void Report(SyntaxNodeAnalysisContext context, SyntaxNode node, INamedTypeSymbol type)
    {
        if (DefinedInNewtonsoft(type))
        {
            context.ReportDiagnostic(Diagnostic, node);
        }
    }

    private static bool DefinedInNewtonsoft(INamedTypeSymbol type)
        => type.ContainingAssembly.Name == "Newtonsoft.Json";
}
