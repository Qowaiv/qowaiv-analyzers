namespace Rules.Define_properties_as_immutables;

public class Verify : System.Xml.Serialization.IXmlSerializable
{
    [Test]
    public void Code()
         => new DefinePropertiesAsImmutables()
        .ForCS()
        .AddSource(@"Cases/DefinePropertiesAsImmutables.cs")
        .AddReference<Qowaiv.DomainModel.EventDispatcher>()
        .AddReference<Qowaiv.Validation.Abstractions.IValidationMessage>()
        .AddReference<System.Xml.Serialization.IXmlSerializable>()
        .Verify();

    public System.Xml.Schema.XmlSchema? GetSchema() => throw new NotImplementedException();

    public void ReadXml(System.Xml.XmlReader reader) => throw new NotImplementedException();

    public void WriteXml(System.Xml.XmlWriter writer) => throw new NotImplementedException();
}
