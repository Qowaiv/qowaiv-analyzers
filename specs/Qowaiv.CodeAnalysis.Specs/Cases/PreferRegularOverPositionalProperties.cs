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

    public sealed record Inherit(string New, string BaseProp) : Base(BaseProp);
    //                           ^^^^^^^^^^

    public record Base(string BaseProp); // Noncompliant
}

namespace Compliant
{
    public sealed record Inherit(string BaseProp) : Base(BaseProp); // Compliant {{Can only besolved after the base has been fixed.}}

    public record Base(string BaseProp); // Noncompliant

    public class ClassWithPrimary(string value) { }

    public record RecordWithoutPrimary
    {
        public string Message { get; init; }
    }

    internal record Internal(string Message);

    public static class Container
    {
        protected record Protected(string Message);

        private record Private(string Message);
    }
    file record File(string Message);

    [System.Obsolete]
    public record ObsoleteCode(string Message);
}
