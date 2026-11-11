namespace Rules.Prefer_XML_LINQ_over_DOM;

public class Verify
{
    [Test]
    public void Rule() => new PreferXmlLinq()
        .ForCS()
        .AddSource(@"Cases/UseSystemXmlLinq.cs")
        .AddReference<System.Xml.XmlDocument>()
        .Verify();

    [Test]
    public void Rule_without_usings() => new PreferXmlLinq()
        .ForCS()
        .AddSources(@"Cases/UseSystemXmlLinq.Gate.cs", @"Cases/UseSystemXmlLinq.GlobalUsings.cs")
        .AddReference<System.Xml.XmlDocument>()
        .Verify();
}
