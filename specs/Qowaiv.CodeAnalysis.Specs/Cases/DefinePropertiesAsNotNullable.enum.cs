#nullable enable

using System;

class Compliant<T> where T : struct, Enum
{
    public string? Name { get; } //            Compliant
    public WithNone Id { get; } //             Compliant
    public int? Number { get; } //             Compliant
    public T Value { get; } //                 Compliant
    public T? NullableValue { get; } //        Compliant {{We can not know if this enum defines an empty state.}}
    public NoMembers NoMembers { get; } //     Compliant
    public NoneNotZero NoneNotZero { get; } // Compliant
    
    private WithNone? Field; //                Compliant
    
    void MethodArguments(WithNone? val) { } // Compliant

    void Variable()
    {
        WithNone? unassigned = default; // Compliant
    }
}

record CompliantRecord(WithNone Value, int? Number);

class Noncompliant
{
    public WithNone? Value { get; } //              Noncompliant {{Define the property as not-nullable as its type has a defined none/empty value.}}
    //     ^^^^^^^^^
    public Nullable<WithNone> Reference { get; } // Noncompliant
    //     ^^^^^^^^^^^^^^^^^^
    
    public WithEmpty? WithEmpty { get; } //         Noncompliant
    public Lowercase? Lowercase { get; } //         Noncompliant
    public Int64Based? Int64Based { get; } //       Noncompliant
    public UInt64Based? UInt64Based { get; } //     Noncompliant
}

record NoncompliantRecordMultiLine(
    WithNone? Value, //             Noncompliant
//  ^^^^^^^^^
    Nullable<WithNone> Reference // Noncompliant
//  ^^^^^^^^^^^^^^^^^^
);

public enum WithNone { None, Other }
public enum Lowercase { none }
public enum WithEmpty { Empty }
public enum Int64Based : long { None }
public enum UInt64Based : ulong { None }

public enum NoMembers { }
public enum NoneNotZero { None = -1 }

