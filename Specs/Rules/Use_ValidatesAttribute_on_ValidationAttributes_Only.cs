namespace Rules.Use_ValidatesAttribute_on_ValidationAttributes_Only;

public class Verify
{
    [Test]
    public void CSharp() => new UseValidatesAttributeOnValidationAttributesOnly()
        .ForCS()
        .AddSource(@"Cases/UseValidatesAttributeOnValidationAttributesOnly.cs")
        .AddReference<System.ComponentModel.DataAnnotations.ValidationAttribute>()
        .AddReference<Qowaiv.Validation.DataAnnotations.ValidatesAttribute>()
        .Verify();
}
