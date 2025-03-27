namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class DecoratePureFunctions() : CodingRule(Rule.DecoratePureFunctions)
{
    protected override void Register(AnalysisContext context)
        => context.RegisterSyntaxNodeAction(Report, SyntaxKind.MethodDeclaration);

    private static void Report(SyntaxNodeAnalysisContext context)
    {
        var declaration = context.Node.Cast<MethodDeclarationSyntax>();

        // The read-only keyword should be enough.
        if (declaration.Modifiers.Any(SyntaxKind.ReadOnlyKeyword)) return;

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
        => type.IsNot(SystemType.System.Void)
        && type.IsNot(SystemType.System.Threading.Task)
        && type.IsNot(SystemType.System.Threading.ValueTask);

    private static bool HasNoRefOutParemeter(IEnumerable<IParameterSymbol> parameters)
        => parameters.All(par => par.RefKind != RefKind.Out && par.RefKind != RefKind.Ref);

    private static bool NoGuard(IMethodSymbol method)
        => !"GUARD".IsContainedBy(method.Name)
        && !"GUARD".IsContainedBy(method.ContainingType.Name);

    private static bool NotDecorated(IEnumerable<AttributeData> attributes)
        => !attributes.Any(attr => Decorated(attr.AttributeClass));

    private static bool Decorated(ITypeSymbol? attr)
        => attr.IsAny(
            SystemType.System.Diagnostics.Contracts.PureAttribute,
            SystemType.System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute)
        || DecoratedImpure(attr!);

    private static bool DecoratedImpure(ITypeSymbol attr)
        => "IMPURE".Matches(attr.Name)
        || "IMPUREATTRIBUTE".Matches(attr.Name)
        || "ASSERTION".IsContainedBy(attr.Name)
        || (attr.BaseType is { } && DecoratedImpure(attr.BaseType));
}
