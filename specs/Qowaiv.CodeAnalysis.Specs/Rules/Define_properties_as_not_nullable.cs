namespace Rules.Define_properties_as_not_nullable;

public class Verify
{
    [Test]
    public void Properties_with_value_types()
        => new DefinePropertiesAsNotNullable()
        .ForCS()
        .AddSource(@"Cases/DefinePropertiesAsNotNullable.svo.cs")
        .AddReference<Qowaiv.Percentage>()
        .Verify();

    [Test]
    public void Properties_with_generics()
       => new DefinePropertiesAsNotNullable()
       .ForCS()
       .AddSource(@"Cases/DefinePropertiesAsNotNullable.generics.cs")
       .AddReference<Qowaiv.Percentage>()
       .Verify();

    [Test]
    public void Properties_with_enums()
       => new DefinePropertiesAsNotNullable()
       .ForCS()
       .AddSource(@"Cases/DefinePropertiesAsNotNullable.enum.cs")
       .Verify();
}
