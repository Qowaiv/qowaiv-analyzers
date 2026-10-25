namespace Fixes.Change_property_type_to_not_nullable;

public class Fixes
{
    [Test]
    public void Nullable_properties()
        => new DefinePropertiesAsNotNullable()
        .ForCS()
        .AddSource(@"Cases/ChangePropertyTypeToNotNullable.ToFix.cs")
        .AddReference<Qowaiv.EmailAddress>()
        .ForCodeFix<ChangePropertyTypeToNotNullable>()
        .AddSource(@"Cases/ChangePropertyTypeToNotNullable.Fixed.cs")
        .Verify();
}
