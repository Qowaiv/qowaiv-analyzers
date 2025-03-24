using System;
using System.ComponentModel.DataAnnotations;

class Noncompliant
{
    [IntOnly] // Noncompliant {{The attribute cannot validate this type}}
//   ^^^^^^^
    public string String { get; init; }

    [Number] // Noncompliant
    public string NoneOfAllowed { get; init; }

    [Generic<Guid>] // Noncompliant
    [Generic<string>]
    public string Generic { get; init; }
}

class Compliant
{
    [IntOnly] // Compliant
    public int Int32 { get; init; }

    [IntOnly] // Compliant
    public int? Nullability { get; init; }

    [Number] // Compliant
    public int AnyOfAllowed1 { get; init; }

    [Number] // Compliant
    public long AnyOfAllowed2 { get; init; }

    [Display(Name = "No validation")]
    public Guid NoValidation { get; init; }

    [Generic<string>] // Compliant
    public string Generic { get; init; }

    [Optional] // Compliant
    public bool? NotDecoratedValidationAttribute { get; init; }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
sealed class ValidatesAttribute : Attribute 
{
    public ValidatesAttribute(Type type) { }

    public ValidatesAttribute() { }

    public bool GenericArgument { get; init; }
}

sealed class OptionalAttribute : ValidationAttribute { }

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
[Validates(GenericArgument = true)]
sealed class GenericAttribute<T> : ValidationAttribute { }

[Validates(typeof(int))]
sealed class IntOnlyAttribute : ValidationAttribute { }

[Validates(typeof(int))]
[Validates(typeof(long))]
sealed class NumberAttribute : ValidationAttribute { }
