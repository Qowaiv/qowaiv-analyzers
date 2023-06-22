using Qowaiv;
using System;
using Alias = System.Nullable<System.Guid>;

class WithQuestionToken
{
    public global::System.Guid GobalDefined { get; }
    public System.Guid WithFullNamespace { get; }
    public Guid TypeOnly { get; }
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
