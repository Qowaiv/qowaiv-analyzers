namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class PreferStronglyTypedIdOverGuid() : PrimitiveObssession(Rule.PreferStronglyTypedIdOverGuid)
{
    protected override bool ShouldNotBePrimitive(PropertyDeclaration property, INamedTypeSymbol type)
        => type.IsAny(SystemType.System.Guid, SystemType.Qowaiv.Uuid)
        || type.NotNullable.IsAny(SystemType.System.Guid, SystemType.Qowaiv.Uuid);
}
