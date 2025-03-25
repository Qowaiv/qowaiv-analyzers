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

    [TypeByString] // Noncompliant
    public string TypeByString { get; init; }
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

    [TypeByString]
    public Guid TypeByString { get; init; }
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

[Validates("System.Guid")]
sealed class TypeByStringAttribute : ValidationAttribute { }


//namespace Qowaiv.Validation.DataAnnotations;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public sealed class ValidatesAttribute : Attribute
{
    /// <summary>Initializes a new instance of the <see cref="ValidatesAttribute"/> class.</summary>
    public ValidatesAttribute() : this(typeof(object)) { }

    /// <summary>Initializes a new instance of the <see cref="ValidatesAttribute"/> class.</summary>
    public ValidatesAttribute(string typeName) => Type = Type.GetType(typeName) ?? typeof(object);

    /// <summary>Initializes a new instance of the <see cref="ValidatesAttribute"/> class.</summary>
    public ValidatesAttribute(Type type) => Type = type;

    /// <summary>Type that can be validated.</summary>
    public Type Type { get; }

    /// <summary>
    /// If true, the type should be equal to the generic to the generic
    /// argument of the <see cref="ValidationAttribute"/>.
    /// </summary>
    public bool GenericArgument { get; init; }
}
