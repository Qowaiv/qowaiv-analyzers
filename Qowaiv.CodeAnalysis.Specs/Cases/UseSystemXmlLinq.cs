using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

class NonCompliant
{
    public void Parameters(
        XmlAttribute attr, //    Noncompliant {{Use XAttribute instead of XmlAttribute}}
//      ^^^^^^^^^^^^
        XmlComment comment, //   Noncompliant {{Use XComment instead of XmlComment}}
        XmlDocument doc, //      Noncompliant {{Use XDocument instead of XmlDocument}}
        XmlDocumentType type, // Noncompliant {{Use XDocumentType instead of XmlDocumentType}}
        XmlDeclaration dec, //   Noncompliant {{Use XDeclaration instead of XmlDeclaration}}
        XmlElement elm, //       Noncompliant {{Use XElement instead of XmlElement}}
        XmlLinkedNode link, //   Noncompliant {{Use XNode instead of XmlLinkedNode}}
        XmlNode node, //         Noncompliant {{Use XNode instead of XmlNode}}
        XmlText text) //         Noncompliant {{Use XText instead of XmlText}}
    {
    }

    private readonly XmlDocument field = new(); // Noncompliant

    public XmlDocument Property { get; init; } = new(); // Noncompliant

    public void Creation()
    {
        var doc = new XmlDocument(); // Noncompliant
    }

    public XmlElement ReturnType() // NonCompliant
    //     ^^^^^^^^^^
        => Property.GetElementById("id")!;
}


class Compliant : IXmlSerializable
{
    public XmlSchema? GetSchema() => null;

    public void Call(NonCompliant model)
    {
        var variable = model.Property; // Compliant {{Calling an other class that returns an XmlDocument}}
    }

    public void ReadXml(XmlReader reader) => reader.MoveToNextAttribute();

    public void WriteXml(XmlWriter writer) => writer.WriteAttributeString("attr", "val");
}
