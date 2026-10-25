#nullable enable

using Qowaiv;
using System;
using Alias = System.Nullable<System.Guid>;

class WithQuestionToken
{
    public global::System.Guid GobalDefined { get; }
    public System.Guid WithFullNamespace { get; }
    public EmailAddress TypeOnly { get; }
    public SomeEnum SomeEnum { get; }
}

class WithNullableGenerics
{
    public global::System.Guid GobalDefined { get; }
    public System.Guid WithFullNamespace { get; }
    public Guid TypeOnly { get; }
}

class WithNullableGenericsAlternatives
{
    public Guid GobalDefined { get; }
    public Guid WithFullNamespace { get; }
    public Guid TypeOnly { get; }
    public System.Guid WithAlias { get; }
}

class WithGenerics
{
    public Guid[] Ids { get; }
    public Dictionary<Guid, SomeEnum> Enums { get; }
    public Guid Nested { get; }
}

record WithNullables(EmailAddress Email, Guid Id);

public enum SomeEnum { None }
