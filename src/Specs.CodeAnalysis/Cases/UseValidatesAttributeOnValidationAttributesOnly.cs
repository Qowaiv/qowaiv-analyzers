using System;
using Qowaiv.Validation.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace NonCompliant
{
    [Validates(typeof(string))]
    public sealed class DecoratedAttribute : Attribute; // Noncompliant

    [Validates(typeof(string))]
    public class DecoratedClass; // Noncompliant

    [Validates(typeof(string))]
    public record DecoratedRecord; // Noncompliant
}

namespace Compliant
{
    [Validates(typeof(string))]
    public sealed class DecoratedValidationAttribute : ValidationAttribute // Compliant
    {
        public override bool IsValid(object? value) => true;
    }

    public abstract class SomeAttribute : Attribute; // Compliant
    
    public class SomeClass; // Compliant

    public record SomeRecord; // Compliant
}
