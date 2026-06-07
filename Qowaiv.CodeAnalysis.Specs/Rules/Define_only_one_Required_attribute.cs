namespace Rules.Define_only_one_Required_attribute;

public class Verify
{
    [Test]
    public void Required_properties() => new DefineOnlyOneRequiredAttribute()
        .ForCS()
        .AddSource(@"Cases/DefineOnlyOneRequiredAttribute.cs")
        .AddReference<System.ComponentModel.DataAnnotations.RequiredAttribute>()
        .Verify();
}
