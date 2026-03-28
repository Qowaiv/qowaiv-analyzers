using System.Xml;

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

    private readonly XmlDocument declaration = new(); // Noncompliant

    public XmlDocument Property { get; init; } = new(); // Noncompliant
}
