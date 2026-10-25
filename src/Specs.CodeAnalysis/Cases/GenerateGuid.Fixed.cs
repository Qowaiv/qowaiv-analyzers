using System;

class Model
{
    public void Ctor()
    {
        Guid impl = Guid.Parse("1f570b45-8b18-496a-84a3-634db4f4c552");
        var expl = Guid.Parse("1f570b45-8b18-496a-84a3-634db4f4c552");
    }

    public void DefaultCtor()
    {
        Guid guid = Guid.Parse("1f570b45-8b18-496a-84a3-634db4f4c552");
        Guid half = Guid.Parse("1f570b45-8b18-496a-84a3-634db4f4c552")    }

    public Guid EmptyString() => Guid.Parse("1f570b45-8b18-496a-84a3-634db4f4c552");
    public Guid MissingArgument() => Guid.Parse("1f570b45-8b18-496a-84a3-634db4f4c552");
    public Guid OnlyOpening() => Guid.Parse("1f570b45-8b18-496a-84a3-634db4f4c552");
}
