using System;

class Noncompliant
{
    public void Ctor()
    {
        Guid impl = new("qowaiv"); //     Error {{"qowaiv" does not represent a GUID}}
        //              ^^^^^^^^
        var expl = new Guid("qowaiv"); // Error {{"qowaiv" does not represent a GUID}}
    }

    public void DefaultCtor()
    {
        Guid guid = new(); // Noncompliant{{Use Guid.Parse() or Guid.Empty instead}}
        //          ^^^^^
        Guid half = new(   // Error[CS1026, CS1002]
                           // Noncompliant@-1 {{Use Guid.Parse() or Guid.Empty instead}}
    }

    public Guid EmptyString() => Guid.Parse(""); //   Error {{"" does not represent a GUID}}
        
    public Guid MissingArgument() => Guid.Parse(); // Error[CS1501]
    //                               ^^^^^^^^^^       {{Provide a GUID value}}

    public Guid OnlyOpening() => Guid.Parse(; //      Error[CS1501, CS1026]
    //                           ^^^^^^^^^^           {{Provide a GUID value}}
}

class Compliant
{
    public void Ctor()
    {
        Guid guid = new("1d4f42fb-ec75-4a99-8c4f-6013bffe98ba"); // Compliant
        var bytes = new Guid([0xBF, 0x9A, 0x5D, 0xA0, 0xDB, 0x23, 0xDB, 0x4F, 0x9E, 0x10, 0xF7, 0xCD, 0x73, 0x9C, 0x25, 0x66]);
    }

    public void Parse()
    {
        var guid = Guid.Parse("9594e82d-ed80-45ce-9603-5d8dd34fcf73"); // Compliant
    }
}
