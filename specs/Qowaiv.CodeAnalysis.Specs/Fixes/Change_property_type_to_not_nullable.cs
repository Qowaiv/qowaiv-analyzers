namespace Fixes.Change_property_type_to_not_nullable;

public class Fixes
{
    [Test]
    public void Code()
        => new DefinePropertiesAsNotNullable()
        .ForCS()
        .AddSource(@"Cases/ChangePropertyTypeToNotNullable.ToFix.cs")
        .ForCodeFix<ChangePropertyTypeToNotNullable>()
        .AddSource(@"Cases/ChangePropertyTypeToNotNullable.Fixed.cs")
        .Verify();
}
