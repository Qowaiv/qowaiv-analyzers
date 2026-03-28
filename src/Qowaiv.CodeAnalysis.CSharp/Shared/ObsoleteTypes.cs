namespace Qowaiv.CodeAnalysis.Shared;

public abstract class ObsoleteTypes(ImmutableArray<SyntaxKind> syntaxKinds, DiagnosticDescriptor supportedDiagnostic, params DiagnosticDescriptor[] additional) 
    : CodingRule(supportedDiagnostic, additional)
{
    protected abstract void Report(SyntaxNodeAnalysisContext context, TypeSyntax node, INamedTypeSymbol type);

    protected sealed override void Register(AnalysisContext context)
    {
        foreach (var kind in SyntaxKinds)
        {
            context.RegisterSyntaxNodeAction(
                kind switch
                {
                    SyntaxKind.FieldDeclaration => ReportField,
                    SyntaxKind.MethodDeclaration => ReportMethod,
                    SyntaxKind.ParameterList => ReportParameterList,
                    SyntaxKind.ObjectCreationExpression => ReportObjectCreation,
                    SyntaxKind.PropertyDeclaration => ReportProperty,
                    _ => throw new NotSupportedException($"Sytnax Kind {kind} is not supported."),
                },
                kind);
        }
    }

    private void ReportField(SyntaxNodeAnalysisContext context)
        => Report(context.Node.Cast<FieldDeclarationSyntax>().Declaration?.Type, context);

    private void ReportMethod(SyntaxNodeAnalysisContext context)
        => Report(context.Node.Cast<MethodDeclarationSyntax>().ReturnType, context);

    private void ReportObjectCreation(SyntaxNodeAnalysisContext context)
        => Report(context.Node.Cast<ObjectCreationExpressionSyntax>().Type, context);

    private void ReportParameterList(SyntaxNodeAnalysisContext context)
    {
        foreach (var type in context.Node.Cast<ParameterListSyntax>().Parameters.Select(p => p.Type))
        {
            Report(type, context);
        }
    }

    private void ReportProperty(SyntaxNodeAnalysisContext context)
        => Report(context.Node.Cast<PropertyDeclarationSyntax>().Type, context);

    private void Report(TypeSyntax? syntax, SyntaxNodeAnalysisContext context)
    {
        foreach (var sub in syntax.SubTypes())
        {
            if (context.SemanticModel.GetSymbolInfo(sub).Symbol is INamedTypeSymbol type)
            {
                Report(context, sub, type);
            }
        }
    }

    private readonly ImmutableArray<SyntaxKind> SyntaxKinds = syntaxKinds;
}
