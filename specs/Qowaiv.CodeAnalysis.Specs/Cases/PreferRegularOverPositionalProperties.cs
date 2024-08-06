namespace Noncompliant
{
    public record PreferRegular(
        string Required, //             Noncompliant {{Define Required as a required property.}}
        string? Optional, //            Noncompliant {{Define Optional as a regular property.}}
        string? Default = "default");// Noncompliant {{Define Default as a property with a default value.}}

    public record PreferRequiredMultipleProperties(string Message, int Value)
    //                                             ^^^^^^^^^^^^^^
    //                                                             ^^^^^^^^^ @-1
    {
    }
}

namespace Compliant
{
    public class ClassWithPrimary(string value) { }

    public record RecordWithoutPrimary
    {
        public string Message { get; init; }
    }
}
