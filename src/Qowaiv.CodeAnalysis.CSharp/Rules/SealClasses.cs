using Microsoft.CodeAnalysis;

namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class SealClasses : DiagnosticAnalyzer
{
    public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = new[]
    {
        Rule.SealClasses,
        Rule.OnlyConcreteClassesDecoratedInheritable
    }
    .ToImmutableArray();

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(Report, SyntaxKind.ClassDeclaration, SyntaxKind.RecordDeclaration);
    }

    private static void Report(SyntaxNodeAnalysisContext context)
        => _ = ReportUnsealedClasses(context) 
        || ReportInvalidDecorations(context);

    private static bool ReportUnsealedClasses(SyntaxNodeAnalysisContext context)
    {
        var declaration = context.Node.MethodDeclaration(context.SemanticModel);

        if (declaration.IsConcrete
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

            return true;
        }
        else return false;
    }

    private static bool ReportInvalidDecorations(SyntaxNodeAnalysisContext context)
    {
        var declaration = context.Node.MethodDeclaration(context.SemanticModel);

        if (!declaration.IsConcrete
            && declaration.Symbol is { } type
            && !type.IsObsolete()
            && !type.IsAttribute()
            && Decorated(type.GetAttributes()) is { } decorated
            && declaration.Attributes.FirstOrDefault(a => IsDecorated(a, decorated)) is { } attr)
        {
            context.ReportDiagnostic(
                Rule.OnlyConcreteClassesDecoratedInheritable,
                attr,
                attr.Name()!);
        }
        return true;
    }

    private static bool IsDecorated(AttributeSyntax attr, INamedTypeSymbol decorated)
        => decorated.Name.StartsWith(attr.Name());

    private static bool IsVirtualOrProtected(ISymbol symbol)
        => (symbol.IsVirtual || symbol.IsProtected())
        && !symbol.IsImplicitlyDeclared;

    private static INamedTypeSymbol? Decorated(IEnumerable<AttributeData> attributes)
       => attributes.FirstOrDefault(attr => IsDecorated(attr.AttributeClass!))?.AttributeClass;

    private static bool IsDecorated(INamedTypeSymbol attr)
        => "INHERITABLE" == attr.Name.ToUpperInvariant()
        || "INHERITABLEATTRIBUTE" == attr.Name.ToUpperInvariant()
        || attr.BaseType is { } && IsDecorated(attr.BaseType);
}
