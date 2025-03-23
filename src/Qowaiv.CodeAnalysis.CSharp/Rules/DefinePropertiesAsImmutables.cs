namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class DefinePropertiesAsImmutables() : ImmutablePropertiesBase(Rule.DefinePropertiesAsImmutables)
{
    protected override void Register(AnalysisContext context)
        => context.RegisterSyntaxNodeAction(Report, SyntaxKind.PropertyDeclaration);

    private void Report(SyntaxNodeAnalysisContext context)
    {
        var property = context.Node.PropertyDeclaration(context.SemanticModel);
        if (IsApplicable(property)
            && HasSetter(property)
            && property.TypedNode.AccessorList!.Accessors.FirstOrDefault(a => a.IsKind(SyntaxKind.SetAccessorDeclaration)) is { } accessor)
        {
            context.ReportDiagnostic(Diagnostic, accessor);
        }
    }

    [Pure]
    private static bool HasSetter(PropertyDeclaration declaration)
        => declaration.Accessors.Contains(SyntaxKind.SetAccessorDeclaration);
}
