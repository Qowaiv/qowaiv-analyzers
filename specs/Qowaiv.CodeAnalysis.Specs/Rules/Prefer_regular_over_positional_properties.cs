namespace Rules.Prefer_regular_over_positional_properties;

public class Verify
{
    [Test]
    public void Code()
         => new PreferRegularOverPositionalProperties()
        .ForCS()
        .AddSource(@"Cases/PreferRegularOverPositionalProperties.cs")
        .Verify();
}
