namespace Rules.Use_compliant_valdation_attribute;

public class Verify
{
    [Test]
    public void compliance_of_validation_attributes() => new UseCompliantValdationAttribute()
        .ForCS()
        .AddSource(@"Cases/UseCompliantValdationAttribute.cs")
        .AddReference<System.ComponentModel.DataAnnotations.ValidationAttribute>()
        .Verify();

    [Test]
    public void compliance_of_build_in_validation_attributes() => new UseCompliantValdationAttribute()
        .ForCS()
        .AddSource(@"Cases/UseCompliantValdationAttribute.BuildIn.cs")
        .AddReference<System.ComponentModel.DataAnnotations.ValidationAttribute>()
        .Verify();
}
