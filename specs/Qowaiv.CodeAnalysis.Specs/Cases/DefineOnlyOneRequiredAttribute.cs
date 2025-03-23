﻿using System.ComponentModel.DataAnnotations;

class Model
{
    [Required]
    public string WithSingle { get; init; }

    [Optional]
    public string WithOptional { get; init; }

    [Required]
    [Optional] // Noncompliant {{WithMultiple should not be decorated with more than one required attribute}} 
//   ^^^^^^^^
    public string WithMultiple { get; init; }

    [Required, Optional] // Noncompliant
//             ^^^^^^^^
    public string WithMultipleInLine { get; init; }

    public string WithoutAttributes { get; init; }

    [StringLength(1024)]
    [Display(Name = "With other Attributes")]
    public string WithOtherAttributes { get; init; }

    [StringLength(1024)]
    [Display(Name = "With other Attributes")]
    [Optional]
    public string WithSingleAndOthers { get; init; }

    [StringLength(1024)]
    [Display(Name = "With other Attributes")]
    [Optional]
    [Required] // Noncompliant
    public string WithMultipleAndOthers { get; init; }
}


public sealed class OptionalAttribute : RequiredAttribute
{
    public override bool IsValid(object? value) => true;
}
