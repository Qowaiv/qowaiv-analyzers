using System;

public class Noncompliant
{
    [Key]
    public string Key { get; init; } // Noncompliant {{Use a strongly typed identifier instead}}
    //     ^^^^^^

    [PrimaryKey]
    public string PrimaryKey { get; init; } // Noncompliant

    [ForeignKey]
    public string ForeignKey { get; init; } // Noncompliant

    public int Id { get; init; } // Noncompliant

    public int ID { get; init; } // Noncompliant

    public int EndsWithId { get; init; } // Noncompliant

    public int EndsWithID { get; init; } // Noncompliant

    [Key]
    public uint UInt32 { get; init; } // Noncompliant

    [Key]
    public long Int64 { get; init; } // Noncompliant

    [Key]
    public ulong UInt64 { get; init; } // Noncompliant
}

public class PublicCompliant(int id)
{
    public Guid Id { get; init; } //   Compliant {{Id but not a primitive.}}

    public string Property { get; init; } // Compliant {{Not an ID.}}

    [PrimitiveRequired]
    [Key]
    public int Primitive { get; init; }  //  Compliant {{ID with [PrimitiveRequired].}}

    private class PrivateCompliant
    {
        public int Id { get; init; }
    }

    public void InMethod(Guid id) { }

    protected class ProtectedCompliant
    {
        public int Id { get; init; }
    }
}

internal class InternalCompliant
{
    public int Id { get; init; }
}

internal sealed class KeyAttribute : Attribute;
internal sealed class PrimaryKeyAttribute : Attribute;
internal sealed class ForeignKeyAttribute : Attribute;
internal sealed class PrimitiveRequiredAttribute : Attribute;
