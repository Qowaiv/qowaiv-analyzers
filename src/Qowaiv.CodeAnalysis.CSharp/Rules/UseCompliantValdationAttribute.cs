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
            || !attribute.IsAssignableTo(SystemType.System.ComponentModel.DataAnnotations.ValidationAttribute))
        {
            return true;
        }

        if (!Lookup.TryGetValue(attribute, out var data))
        {
            if (Known.FirstOrDefault(kpv => attribute.Is(kpv.Key)) is { Key: not null } known)
            {
                data = known.Value;
            }
            else
            {
                data = Validates(attribute);
            }
            // If it contains data, its matches everyting.
            Lookup[attribute] = data.Contains(SystemType.System.Object) ? [] : data;
        }

        return data is { Length: 0 } || data.Any(type => member.IsAssignableTo(type) || member.Implements(type));
    }

    private static SystemType[] Validates(INamedTypeSymbol attribute) => [..attribute
        .GetAttributes()
        .Where(data => data.AttributeClass.Is(SystemType.Qowaiv.Validation.DataAnnotations.ValidatesAttribute))
        .Select(data => GetType(data) ?? GetStringType(data) ?? GetGeneric(data, attribute))
        .OfType<SystemType>()];

    private static SystemType? GetType(AttributeData data)
        => data.ConstructorArguments is { Length: 1 } args
        && args[0].Value is ITypeSymbol validates
        ? SystemType.New(validates)
        : null;

    private static SystemType? GetStringType(AttributeData data)
       => data.ConstructorArguments is { Length: 1 } args
       && args[0].Value is string validates
       ? SystemType.Parse(validates)
       : null;

    private static SystemType? GetGeneric(AttributeData data, INamedTypeSymbol attribute)
        => attribute.IsGenericType
        && data.NamedArguments.FirstOrDefault(kvp => kvp.Key == "GenericArgument").Value.Value is true
        ? SystemType.New(attribute.TypeArguments[0])
        : null;

#pragma warning disable RS1008
    // Avoid storing per-compilation data into the fields of a diagnostic analyzer
    // We do this to cache the annoatations on Validation Attributes. This reduces
    // The analysis times.
    private static readonly ConcurrentDictionary<INamedTypeSymbol, SystemType[]> Lookup = [];

    internal static readonly Dictionary<SystemType, SystemType[]> Known = new()
    {
        [SystemType.System.ComponentModel.DataAnnotations.AllowedValuesAttribute] = [SystemType.System.IConvertible],
        [SystemType.System.ComponentModel.DataAnnotations.Base64StringAttribute] = [SystemType.System.String],
        [SystemType.System.ComponentModel.DataAnnotations.CreditCardAttribute] = [SystemType.System.String],
        [SystemType.System.ComponentModel.DataAnnotations.DeniedValuesAttribute] = [SystemType.System.IConvertible],
        [SystemType.System.ComponentModel.DataAnnotations.EmailAddressAttribute] = [SystemType.System.String],
        [SystemType.System.ComponentModel.DataAnnotations.EnumDataTypeAttribute] = [SystemType.System.Enum],
        [SystemType.System.ComponentModel.DataAnnotations.FileExtensionsAttribute] = [SystemType.System.String],
        [SystemType.System.ComponentModel.DataAnnotations.PhoneAttribute] = [SystemType.System.String],
        [SystemType.System.ComponentModel.DataAnnotations.RangeAttribute] = [SystemType.System.IComparable],
        [SystemType.System.ComponentModel.DataAnnotations.StringLengthAttribute] = [SystemType.System.String],
        [SystemType.System.ComponentModel.DataAnnotations.UrlAttribute] = [SystemType.System.String],
    };
}
