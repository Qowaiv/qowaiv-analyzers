#nullable enable

using Qowaiv;
using System.ComponentModel.DataAnnotations;

public class NonCompliant
{
    [Base64String]
    public string Base64 { get; init; } // Noncompliant {{Use System.BinaryData instead}}

    [EmailAddress]
    public string Email { get; init; } // Noncompliant {{Use Qowaiv.EmailAddress instead}}

    [Url]
    public string Url { get; init; } // Noncompliant {{Use System.Uri instead}}

    [Base64String]
    public string? NullableString { get; init; } // Noncompliant
}

public class Compliant
{
    [Base64String]
    public byte[] Base64 { get; init; } // Compliant {{Only check strings}}

    [EmailAddress]
    public EmailAddress Email { get; init; }// Compliant {{Only check strings}}

    [Base64String]
    protected string Base64String { get; init; } // Compliant {{Do not check non-public members}}
}

class Ignored // Compliant {{Do not check non-public classes}}
{
    [Base64String]
    public string Base64 { get; init; }
}
