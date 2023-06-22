using Qowaiv;
using System;

class Compliant<T> where T : struct
{
    public Guid Id { get; } //            Compliant
    public EmailAddress Email { get; } // Compliant
    public int? Number { get; } //        Compliant
    public Percentage? Factor { get; } // Compliant
    public T Value { get; } //            Compliant
    public T? NullableValue { get; } //   Compliant {{We can not know if this struct defines an Empty state.}}
    private Guid? Field; //               Compliant
    void MethodArguments(Guid? id) { } // Compliant

    void Variable()
    {
        EmailAddress? unassigned = default; // Compliant
    }

    public NoMembers? NoMembers { get; } //         Compliant
    public NotReadOnly? NotReadOnly { get; } //     Compliant
    public NotStatic? NotStatic { get; } //         Compliant
    public DifferentType? DifferentType { get; } // Compliant
    public NotPublic? NotPublic { get; } //         Compliant
    public NoField? NoField { get; } //             Compliant
}

class Noncompliant
{
    public Guid? Id { get; } //                 Noncompliant {{Define the property as not-nullable as its type has an empty state.}}
    //     ^^^^^
    public EmailAddress? Email { get; } //      Noncompliant

    public Nullable<Guid> Reference { get; } // Noncompliant
    //     ^^^^^^^^^^^^^^
}

struct NoMembers
{
}
struct NotReadOnly
{
    public static NotReadOnly Empty;
}
struct NotStatic
{
    public readonly NotStatic Empty; // Error[CS0523]
}
struct DifferentType
{
    public static readonly Guid Empty;
}
struct NotPublic
{
    internal static readonly NotPublic Empty;
}
struct NoField
{
    public static NoField Empty() { return default; }
}
