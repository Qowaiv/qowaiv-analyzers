using System;
using Qowaiv;

public class Noncompliant
{
    public Guid GuidId { get; init; } //        Noncompliant {{Use a strongly typed identifier instead}}
    //     ^^^^
    public Uuid UuidId { get; init; } //        Noncompliant

    public Guid? NullableGuid { get; init; } // Noncompliant

    public Uuid? NullableUuid { get; init; } // Noncompliant
}

public class PublicCompliant(Guid id)
{
    public string Id { get; init; } = id.ToString();

    private class PrivateCompliant
    {
        public Guid Id { get; init; }
    }

    public void InMethod(Guid id) { }

    protected class ProtectedCompliant
    {
        public Guid Id { get; init; }
    }
}

internal class InternalCompliant
{
    public Guid Id { get; init; }
}
