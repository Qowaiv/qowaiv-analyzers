namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class SealClasses : DiagnosticAnalyzer
{
    public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => Description.SealClasses.Array();

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(Report, SyntaxKind.ClassDeclaration, SyntaxKind.RecordDeclaration);
    }

    private void Report(SyntaxNodeAnalysisContext context)
    {
        var declaration = context.Node.MethodDeclaration(context.SemanticModel);

        if (declaration.IsConcrete
            && declaration.Symbol is { } type
            && !type.IsObsolete()
            && !type.IsAttribute()
            && !type.GetMembers().Any(IsVirtualOrProtected)
            && NotDecorated(type.GetAttributes()))
        {
            context.ReportDiagnostic(Description.SealClasses, declaration.ChildTokens().First(t => t.IsKind(SyntaxKind.IdentifierToken)));
        }
    }

    private static bool IsVirtualOrProtected(ISymbol symbol)=> symbol.IsVirtual || symbol.IsProtected();
    
    private static bool NotDecorated(IEnumerable<AttributeData> attributes)
       => !attributes.Any(attr => Decorated(attr.AttributeClass));

    private static bool Decorated(INamedTypeSymbol? attr)
        => attr.Is(SystemType.System_Diagnostics_Contracts_PureAttribute)
        || DecoratedInheritable(attr!);

    private static bool DecoratedInheritable(INamedTypeSymbol attr)
        => "INHERITABLE" == attr.Name.ToUpperInvariant()
        || "INHERITABLEATTRIBUTE" == attr.Name.ToUpperInvariant()
        || attr.BaseType is { } && DecoratedInheritable(attr.BaseType);
}
