namespace Rules.Decorate_ValidationAttributes;

public class Verify
{
    [Test]
    public void Rule() => new DecorateValidationAttributes()
        .ForCS()
        .AddSource(@"Cases/DecorateValidationAttributes.cs")
        .AddReference<System.ComponentModel.DataAnnotations.ValidationAttribute>()
        .AddReference<Qowaiv.Validation.DataAnnotations.ValidatesAttribute>()
        .Verify();
}
