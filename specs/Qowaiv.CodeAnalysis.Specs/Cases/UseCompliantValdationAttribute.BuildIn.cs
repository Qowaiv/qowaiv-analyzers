using System;
using System.ComponentModel.DataAnnotations;

class Noncompliant
{
    [AllowedValues(42)] // Noncompliant
    public Guid AllowedValues { get; init; }

    [Base64String] // Noncompliant
    public long Base64String { get; init; }

    [CreditCard] // Noncompliant
    public int CreditCard { get; init; }

    [DeniedValues(1983)] // Noncompliant
    public int DeniedValues { get; init; }

    [EmailAddress] // Noncompliant
    public int EmailAddress { get; init; }

    [EnumDataType(typeof(int))] // Noncompliant
    public int EnumDataType { get; init; }

    [FileExtensions] // Noncompliant
    public int FileExtensions { get; init; }

    [Phone] // Noncompliant
    public int Phone { get; init; }

    [Range(3, 4)] // Noncompliant
    public object Range { get; init; }

    [StringLength(3)] // Noncompliant
    public Guid StringLength { get; init; }

    [Url] // Noncompliant
    public Guid Url { get; init; }
}

class Compliant
{
    [AllowedValues(43)]
    public int AllowedValues { get; init; }

    [Base64String]
    public string Base64String { get; init; }

    [CreditCard] 
    public string CreditCard { get; init; }

    [DeniedValues(1983)] 
    public string DeniedValues { get; init; }

    [EmailAddress]
    public int EmailAddress { get; init; }

    [EnumDataType(typeof(MyEnum))]
    public MyEnum EnumDataType { get; init; }

    [FileExtensions]
    public string FileExtensions { get; init; }

    [Phone]
    public string Phone { get; init; }

    [Range(3, 4)]
    public int Range { get; init; }

    [StringLength(3)]
    public string StringLength { get; init; }

    [Url]
    public string Url { get; init; }
}

public enum MyEnum { }
