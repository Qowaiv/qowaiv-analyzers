using System.Collections.Concurrent;

namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UseCompliantValdationAttribute() : CodingRule(Rule.UseCompliantValdationAttribute)
{
    protected override void Register(AnalysisContext context)
        => context.RegisterSyntaxNodeAction(
            Report,
            SyntaxKind.PropertyDeclaration,
            SyntaxKind.FieldDeclaration,
            SyntaxKind.Parameter);

    private void Report(SyntaxNodeAnalysisContext context)
    {
        var member = context.Node.MemberDeclaration(context.SemanticModel);

        if (member.Attributes.Any() && member.Symbol is { } mSymbol)
        {
            mSymbol = mSymbol.NotNullable() ?? mSymbol;

            foreach (var attribute in member.Attributes.Where(a => !CanValidate(mSymbol, a.Symbol)))
            {
                context.ReportDiagnostic(Diagnostic, attribute);
            }
        }
    }

    private static bool CanValidate(INamedTypeSymbol member, INamedTypeSymbol? attribute)
    {
        if (attribute is null
            || !attribute.IsAssignableTo(SystemType.System_ComponentModel_DataAnnotations_ValidationAttribute))
        {
            return true;
        }

        if (!Lookup.TryGetValue(attribute, out var data))
        {
            data = Validates(attribute);
            Lookup[attribute] = data;
        }

        return data is { Length: 0 } || data.Any(member.IsAssignableTo);
    }

    private static ITypeSymbol[] Validates(INamedTypeSymbol attribute) => [..attribute
        .GetAttributes()
        .Where(data => data.AttributeClass?.Name.Matches("ValidatesAttribute") is true)
        .Select(data => GetType(data) ?? GetGeneric(data, attribute))
        .OfType<ITypeSymbol>()];

    private static ITypeSymbol? GetType(AttributeData data)
        => data.ConstructorArguments is { Length: 1 } args
        && args[0].Value is ITypeSymbol validates
        ? validates
        : null;

    private static ITypeSymbol? GetGeneric(AttributeData data, INamedTypeSymbol attribute)
        => attribute.IsGenericType
        && data.NamedArguments.FirstOrDefault(kvp => kvp.Key == "GenericArgument").Value.Value is true
        ? attribute.TypeArguments[0]
        : null;

#pragma warning disable RS1008 
    // Avoid storing per-compilation data into the fields of a diagnostic analyzer
    // We do this to cache the annoatations on Validation Attributes. This reduces
    // The analysis times.
    private static readonly ConcurrentDictionary<ITypeSymbol, ITypeSymbol[]> Lookup = [];
}
