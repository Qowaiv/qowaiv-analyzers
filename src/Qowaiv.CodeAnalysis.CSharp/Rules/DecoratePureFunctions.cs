namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class DecoratePureFunctions() : CodingRule(Rule.DecoratePureFunctions)
{
    protected override void Register(AnalysisContext context)
        => context.RegisterSyntaxNodeAction(Report, SyntaxKind.MethodDeclaration);

    private void Report(SyntaxNodeAnalysisContext context)
    {
        var declaration = context.Node;
        var symbol = context.SemanticModel.GetDeclaredSymbol(declaration);

        if (symbol is IMethodSymbol method
            && ReturnsResult(method.ReturnType)
            && NoGuard(method)
            && HasNoRefOutParemeter(method.Parameters)
            && !method.IsObsolete()
            && NotDecorated(method.GetAttributes()))
        {
            context.ReportDiagnostic(Rule.DecoratePureFunctions, declaration.ChildTokens().First(t => t.IsKind(SyntaxKind.IdentifierToken)));
        }
    }

    private static bool ReturnsResult(ITypeSymbol type)
        => type.IsNot(SystemType.System_Void)
        && type.IsNot(SystemType.System_Threading_Task)
        && type.IsNot(SystemType.System_Threading_ValueTask);

    private static bool HasNoRefOutParemeter(IEnumerable<IParameterSymbol> parameters)
        => parameters.All(par => par.RefKind != RefKind.Out && par.RefKind != RefKind.Ref);

    private static bool NoGuard(IMethodSymbol method)
        => !"GUARD".IsContainedBy(method.Name)
        && !"GUARD".IsContainedBy(method.ContainingType.Name);

    private static bool NotDecorated(IEnumerable<AttributeData> attributes)
        => !attributes.Any(attr => Decorated(attr.AttributeClass));

    private static bool Decorated(ITypeSymbol? attr)
        => attr.IsAny(
            SystemType.System_Diagnostics_Contracts_PureAttribute,
            SystemType.System_Diagnostics_CodeAnalysis_DoesNotReturnAttribute)
        || DecoratedImpure(attr!);

    private static bool DecoratedImpure(ITypeSymbol attr)
        => "IMPURE".Matches(attr.Name)
        || "IMPUREATTRIBUTE".Matches(attr.Name)
        || "ASSERTION".IsContainedBy(attr.Name)
        || (attr.BaseType is { } && DecoratedImpure(attr.BaseType));
}
