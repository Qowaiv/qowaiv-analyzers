namespace Qowaiv.CodeAnalysis.Rules;

/// <summary>Implements <see cref="Rule.UseSystemDateOnly"/>.</summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UseSystemDateOnly() : ObsoleteTypes(
    [
        SyntaxKind.FieldDeclaration,
        SyntaxKind.MethodDeclaration,
        SyntaxKind.ParameterList,
        SyntaxKind.PropertyDeclaration,
    ]
    , Rule.UseSystemDateOnly)
{
    protected override void Report(SyntaxNodeAnalysisContext context, SyntaxNode node, INamedTypeSymbol type)
    {
        if ((type.NotNullable() ?? type).Is(SystemType.Qowaiv.Date))
        {
            context.ReportDiagnostic(Diagnostic, node);
        }
    }
}
