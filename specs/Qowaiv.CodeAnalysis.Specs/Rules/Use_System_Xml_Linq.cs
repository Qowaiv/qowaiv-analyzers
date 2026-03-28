namespace Rules.Use_System_Xml_Linq;

public class Verify
{
    [Test]
    public void Rule() => new UseLinqToXml()
        .ForCS()
        .AddSource(@"Cases/UseSystemXmlLinq.cs")
        .AddReference<System.Xml.XmlDocument>()
        .Verify();
}
