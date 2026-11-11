namespace Gate
{
    public class ViaGlobalUsing
    {
        public Newtonsoft.Json.Linq.JToken Token { get; set; } // Noncompliant {{Use System.Text.Json for JSON serialization}}

        public JArray Array { get; set; } // Noncompliant {{Use System.Text.Json for JSON serialization}}
    }
}

namespace Gate.Compliant
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class Compliant
    {
        [JsonPropertyName("value")] // Compliant {{System.Text.Json.}}
        public string Value { get; set; }
    }
}
