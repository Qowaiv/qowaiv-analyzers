namespace Gate
{
    using Doc = System.Xml.XmlDocument;

    public class ViaAlias
    {
        public Doc Document { get; set; } // Noncompliant {{Use XDocument instead of XmlDocument}}
        //     ^^^

        public Doc[] Documents { get; set; } // Noncompliant {{Use XDocument instead of XmlDocument}}
    }

    public class FullyQualified
    {
        public System.Xml.XmlNode Node { get; set; } // Noncompliant {{Use XNode instead of XmlNode}}
        //     ^^^^^^^^^^^^^^^^^^

        public global::System.Xml.XmlText Text; // Noncompliant {{Use XText instead of XmlText}}
    }
}

namespace System.Xml
{
    // The involved types are declared in that namespace itself: no using needed.
    public class InNamespace
    {
        public XmlElement Element { get; set; } // Noncompliant {{Use XElement instead of XmlElement}}
        //     ^^^^^^^^^^
    }
}

namespace Gate
{
    // The involved types come from the global usings of the other file.
    using System.Collections.Generic;

    public class ViaGlobalUsing
    {
        public XmlComment Comment { get; set; } // Noncompliant {{Use XComment instead of XmlComment}}
        //     ^^^^^^^^^^

        public Attr Attribute { get; set; } // Noncompliant {{Use XAttribute instead of XmlAttribute}}
        //     ^^^^

        public List<Attr> Attributes { get; set; } // Noncompliant {{Use XAttribute instead of XmlAttribute}}
    }

    public class Compliant
    {
        public MyXmlDocument Document { get; set; } // Compliant {{A type of its own.}}
        public System.Xml.XmlReader Reader { get; set; } // Compliant {{Not replaced by LINQ to XML.}}
    }
}

public class MyXmlDocument { }
