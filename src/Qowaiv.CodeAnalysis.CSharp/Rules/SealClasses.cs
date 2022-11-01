namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class SealClasses : DiagnosticAnalyzer
{
    public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = new[]
    {
        Rule.SealClasses,
        Rule.OnlyUnsealedConcreteClassesCanBeInheritable
    }
    .ToImmutableArray();

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(Report, SyntaxKind.ClassDeclaration, SyntaxKind.RecordDeclaration);
    }

    private static void Report(SyntaxNodeAnalysisContext context)
    {
        var declaration = context.Node.MethodDeclaration(context.SemanticModel);
        ReportUnsealedClasses(declaration, context);
        ReportInvalidDecorations(declaration, context);
    }

    private static void ReportUnsealedClasses(MethodDeclaration declaration, SyntaxNodeAnalysisContext context)
    {
        if (declaration.IsConcrete 
            && !declaration.IsSealed
            && declaration.Symbol is { } type
            && !type.IsObsolete()
            && !type.IsAttribute()
            && !type.GetMembers().Any(IsVirtualOrProtected)
            && Decorated(type.GetAttributes()) is null)
        {
            context.ReportDiagnostic(
                Rule.SealClasses,
                declaration.ChildTokens().First(t => t.IsKind(SyntaxKind.IdentifierToken)),
                declaration.IsRecord ? "record" : "class");
        }
    }

    private static void ReportInvalidDecorations(MethodDeclaration declaration, SyntaxNodeAnalysisContext context)
    {
        if ((!declaration.IsConcrete || declaration.IsSealed)
            && declaration.Symbol is { } type
            && !type.IsObsolete()
            && !type.IsAttribute()
            && Decorated(type.GetAttributes()) is { } decorated
            && declaration.Attributes.FirstOrDefault(a => IsDecorated(a, decorated)) is { } attr)
        {
            context.ReportDiagnostic(
                Rule.OnlyUnsealedConcreteClassesCanBeInheritable,
                attr,
                attr.Name()!);
        }
    }

    [Pure]
    private static bool IsDecorated(AttributeSyntax attr, INamedTypeSymbol decorated)
        => decorated.Name.StartsWith(attr.Name());

    [Pure]
    private static bool IsVirtualOrProtected(ISymbol symbol)
        => (symbol.IsVirtual || symbol.IsProtected())
        && !symbol.IsImplicitlyDeclared;

    [Pure]
    private static INamedTypeSymbol? Decorated(IEnumerable<AttributeData> attributes)
       => attributes.FirstOrDefault(attr => IsDecorated(attr.AttributeClass!))?.AttributeClass;

    [Pure]
    private static bool IsDecorated(INamedTypeSymbol attr)
        => "INHERITABLE" == attr.Name.ToUpperInvariant()
        || "INHERITABLEATTRIBUTE" == attr.Name.ToUpperInvariant()
        || attr.BaseType is { } && IsDecorated(attr.BaseType);
}
