namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class SealClasses() : CodingRule(
    Rule.SealClasses,
    Rule.OnlyUnsealedConcreteClassesCanBeInheritable)
{
    protected override void Register(AnalysisContext context)
        => context.RegisterSyntaxNodeAction(Report, SyntaxKind.ClassDeclaration, SyntaxKind.RecordDeclaration);

    private static void Report(SyntaxNodeAnalysisContext context)
    {
        var declaration = context.Node.TypeDeclaration(context.SemanticModel);
        ReportUnsealedClasses(declaration, context);
        ReportInvalidDecorations(declaration, context);
    }

    private static void ReportUnsealedClasses(TypeDeclaration declaration, SyntaxNodeAnalysisContext context)
    {
        if (declaration.IsConcrete
            && !declaration.IsSealed
            && declaration.Symbol is { } type
            && !type.IsObsolete()
            && !type.IsAttribute()
            && !type.IsException()
            && !type.GetMembers().Any(IsVirtualOrProtected)
            && Decorated(type.GetAttributes()) is null)
        {
            context.ReportDiagnostic(
                Rule.SealClasses,
                declaration.ChildTokens().First(t => t.IsKind(SyntaxKind.IdentifierToken)),
                declaration.IsRecord ? "record" : "class");
        }
    }

    private static void ReportInvalidDecorations(TypeDeclaration declaration, SyntaxNodeAnalysisContext context)
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
        => !symbol.IsImplicitlyDeclared && (symbol.IsVirtual || IsProtected(symbol));

    [Pure]
    private static bool IsProtected(ISymbol symbol)
        => symbol.IsProtected() && !symbol.IsOverride;

    [Pure]
    private static INamedTypeSymbol? Decorated(IEnumerable<AttributeData> attributes)
       => attributes.FirstOrDefault(attr => IsDecorated(attr.AttributeClass!))?.AttributeClass;

    [Pure]
    private static bool IsDecorated(INamedTypeSymbol attr)
        => "INHERITABLE".Matches(attr.Name)
        || "INHERITABLEATTRIBUTE".Matches(attr.Name)
        || (attr.BaseType is { } && IsDecorated(attr.BaseType));
}
