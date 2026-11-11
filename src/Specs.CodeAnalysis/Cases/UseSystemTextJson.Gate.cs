namespace Gate
{
    // No using directives: all references are fully qualified.

    public class Annotated
    {
        [Newtonsoft.Json.JsonProperty("value")] // Noncompliant {{Use System.Text.Json for JSON serialization}}
        public string Value { get; set; }

        public Newtonsoft.Json.JsonSerializer Serializer { get; set; } // Noncompliant {{Use System.Text.Json for JSON serialization}}

        public void Convert(Newtonsoft.Json.JsonReader reader) { } // Noncompliant {{Use System.Text.Json for JSON serialization}}
    }

    public class Custom : Newtonsoft.Json.JsonException // Noncompliant {{Use System.Text.Json for JSON serialization}}
    {
    }
}

namespace Gate.Compliant
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    // No Newtonsoft.Json in scope: nothing is bound at all.
    public class Compliant
    {
        [JsonPropertyName("value")] // Compliant {{System.Text.Json.}}
        public string Value { get; set; }

        public JsonSerializerOptions Options { get; set; } // Compliant {{System.Text.Json.}}
    }
}
