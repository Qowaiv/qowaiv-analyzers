namespace Rules.Prefer_XML_LINQ_over_DOM;

public class Verify
{
    [Test]
    public void Rule() => new PreferXmlLinq()
        .ForCS()
        .AddSource(@"Cases/UseSystemXmlLinq.cs")
        .AddReference<System.Xml.XmlDocument>()
        .Verify();
}
