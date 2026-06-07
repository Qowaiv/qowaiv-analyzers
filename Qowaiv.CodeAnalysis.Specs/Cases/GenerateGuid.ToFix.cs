using System;

class Model
{
    public void Ctor()
    {
        Guid impl = new("qowaiv");
        var expl = new Guid("qowaiv");
    }

    public void DefaultCtor()
    {
        Guid guid = new();
        Guid half = new(
    }

    public Guid EmptyString() => Guid.Parse("");
    public Guid MissingArgument() => Guid.Parse();
    public Guid OnlyOpening() => Guid.Parse(;
}
