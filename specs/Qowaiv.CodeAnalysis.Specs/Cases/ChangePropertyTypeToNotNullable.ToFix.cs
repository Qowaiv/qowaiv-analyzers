#nullable enable

using Qowaiv;
using System;
using Alias = System.Nullable<System.Guid>;

class WithQuestionToken
{
    public global::System.Guid? GobalDefined { get; }
    public System.Guid? WithFullNamespace { get; }
    public Guid? TypeOnly { get; }
}

class WithNullableGenerics
{
    public Nullable<global::System.Guid> GobalDefined { get; }
    public Nullable<System.Guid> WithFullNamespace { get; }
    public Nullable<Guid> TypeOnly { get; }
}

class WithNullableGenericsAlternatives
{
    public global::System.Nullable<Guid> GobalDefined { get; }
    public System.Nullable<Guid> WithFullNamespace { get; }
    public Nullable<Guid> TypeOnly { get; }
    public Alias WithAlias { get; }
}

record WithNullables(string? Name, Guid? Id, EmailAddress? Email);
