namespace Rules.Required_attribute_cannot_invalidate_value_types;

public class Verify
{
    [Test]
    public void Required_properties() => new RequiredAttributeCannotInvalidateValueTypes()
        .ForCS()
        .AddSource(@"Cases/RequiredAttributeCannotInvalidateValueTypes.cs")
        .AddReference<System.ComponentModel.DataAnnotations.RequiredAttribute>()
        .Verify();
}
