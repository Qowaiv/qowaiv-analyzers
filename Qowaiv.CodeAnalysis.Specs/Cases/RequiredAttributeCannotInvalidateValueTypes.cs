using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

class OnProperties
{
    [Required] // Compliant
    public string NonValueType { get; init; }

    [Optional] // Compliant
    public int DerivedRequired { get; init; }

    [Required] // Compliant
    public int? Nullable { get; init; }

    [Required] // Noncompliant {{The value of this value type will always meet the Required constraints}}
//   ^^^^^^^^     
    public int Integer { get; init; }

    [Required] // Noncompliant
    public System.Guid Guid { get; init; }

    [Required] // Noncompliant
    public MyEnum Enum { get; init; }
}

class OnFields
{
    [Required] // Noncompliant
    public int ValueType;
}

record OnRecords([Required] int ValueType, [Required] string WithSingle); // Noncompliant
//                ^^^^^^^^

public enum MyEnum { }

public sealed class OptionalAttribute : RequiredAttribute { }

[System.Text.Json.Serialization.JsonNumberHandling(JsonNumberHandling.Strict)]
class JsonSerializableObject
{
    [Required]
    public int ValueType { get; init; } // Compliant
}

class JsonSerializableProperty
{
    [System.Text.Json.Serialization.JsonPropertyName("value")]
    [Required]
    public int ValueType { get; init; } // Compliant
}

class NewtonsoftSerializable
{
    [Required]
    public int ValueType { get; init; } // Compliant

    [Newtonsoft.Json.JsonProperty("value")]
    public string Value { get; init; }
}
