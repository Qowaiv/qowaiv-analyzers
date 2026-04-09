using System;
using Qowaiv.Validation.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace NonCompliant
{
    public sealed class NotDecoratedValidationAttribute : ValidationAttribute // Noncompliant {{The attribute lacks a [Validates(Type)] attribute}}
    //                  ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
    {
        public override bool IsValid(object? value) => true;
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DecoratedWithUsageAttribute: ValidationAttribute // Noncompliant
    {
        public override bool IsValid(object? value) => true;
    }
}

namespace Compliant
{
    [Validates(typeof(string))]
    public sealed class DecoratedValidationAttribute : ValidationAttribute // Compliant
    {
        public override bool IsValid(object? value) => true;
    }

    public abstract class NotDecoratedAbstractValidationAttribute : ValidationAttribute // Compliant
    {
        public override bool IsValid(object? value) => true;
    }

    public class SomeAttribute : Attribute;

    public class SomeClass;
}
