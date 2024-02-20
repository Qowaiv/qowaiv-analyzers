namespace Rules.Use_immutable_types_for_properties;

public class Verify
{
    [Test]
    public void Code()
         => new UseImmutableTypesForProperties()
        .ForCS()
        .AddSource(@"Cases/UseImmutableTypesForProperties.cs")
        .Verify();
}
