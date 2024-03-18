namespace Rules.Define_properties_as_immutables;

public class Verify
{
    [Test]
    public void Code()
         => new DefinePropertiesAsImmutables()
        .ForCS()
        .AddSource(@"Cases/DefinePropertiesAsImmutables.cs")
        .AddReference<Qowaiv.DomainModel.EventDispatcher>()
        .AddReference<Qowaiv.Validation.Abstractions.IValidationMessage>()
        .Verify();
}
