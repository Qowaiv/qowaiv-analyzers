namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class DecoratePureFunctions : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = Rule.DecoratePureFunctions.Array();

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(Report, SyntaxKind.MethodDeclaration);
    }

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

    private bool ReturnsResult(ITypeSymbol type)
        => type.IsNot(SystemType.System_Void)
        && type.IsNot(SystemType.System_Threading_Task)
        && type.IsNot(SystemType.System_Threading_ValueTask);

    private static bool HasNoRefOutParemeter(IEnumerable<IParameterSymbol> parameters)
        => parameters.All(par => par.RefKind != RefKind.Out && par.RefKind != RefKind.Ref);

    private static bool NoGuard(IMethodSymbol method)
        => !method.Name.ToUpperInvariant().Contains("GUARD")
        && method.ContainingType.Name.ToUpperInvariant() != "GUARD";

    private static bool NotDecorated(IEnumerable<AttributeData> attributes)
        => !attributes.Any(attr => Decorated(attr.AttributeClass));

    private static bool Decorated(ITypeSymbol? attr)
        => attr.Is(SystemType.System_Diagnostics_Contracts_PureAttribute)
        || DecoratedImpure(attr!);

    private static bool DecoratedImpure(ITypeSymbol attr)
        => "IMPURE" == attr.Name.ToUpperInvariant()
        || "IMPUREATTRIBUTE" == attr.Name.ToUpperInvariant()
        || attr.Name.ToUpperInvariant().Contains("ASSERTION")
        || attr.BaseType is { } && DecoratedImpure(attr.BaseType);
}
