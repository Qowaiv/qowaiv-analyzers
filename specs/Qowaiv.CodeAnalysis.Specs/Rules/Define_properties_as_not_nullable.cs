namespace Rules.Define_properties_as_not_nullable;

public class Verify
{
    [Test]
    public void Properties_with_value_types()
        => new DefinePropertiesAsNotNullable()
        .ForCS()
        .AddSource(@"Cases/DefinePropertiesAsNotNullable.cs")
        .AddReference<Qowaiv.Percentage>()
        .Verify();
}
