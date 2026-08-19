using Qowaiv;

class Noncompliant
{
    public void DefaultCtor()
    {
        Uuid uuid = new(); // Noncompliant{{Use Uuid.Parse() or Uuid.Empty instead}}
        //          ^^^^^
        Uuid half = new(   // Error[CS1026, CS1002]
                           // Noncompliant@-1 {{Use Uuid.Parse() or Uuid.Empty instead}}
    }

    public Uuid EmptyString() => Uuid.Parse(""); //   Error {{"" does not represent a UUID}}
        
    public Uuid MissingArgument() => Uuid.Parse(); // Error[CS1501]
    //                               ^^^^^^^^^^       {{Provide a UUID value}}

    public Uuid OnlyOpening() => Uuid.Parse(; //      Error[CS1501, CS1026]
    //                           ^^^^^^^^^^           {{Provide a UUID value}}
}

class Compliant
{
    public void Parse()
    {
        var uuid = Uuid.Parse("Qowaiv_SVOLibrary_GUIA"); // Compliant
    }
}
