namespace Fixes.Convert_positional_properties;

public class Fixes
{
    [Test]
    public void Code()
        => new PreferRegularOverPositionalProperties()
        .ForCS()
        .AddSource(@"Cases/ConvertPositionalProperties.ToFix.cs")
        .ForCodeFix<ConvertPositionalProperties>()
        .AddSource(@"Cases/ConvertPositionalProperties.Fixed.cs")
        .Verify();
}
