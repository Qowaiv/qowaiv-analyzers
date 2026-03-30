using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Noncompliant
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)] // Noncompliant
    [JsonConverter(typeof(CustomJsonConverter))] // Noncompliant
    public class  AnnotatedModel
    {
        [JsonConstructor] // Noncompliant {{Use System.Text.Json for JSON serialization}}
//       ^^^^^^^^^^^^^^^
        public AnnotatedModel(string value) => Value = value;

        [JsonProperty("value")] // Noncompliant
        public string Value { get; }

        [JsonIgnore] // Noncompliant
        public object Ignore { get; init; }

        [JsonExtensionData] // Noncompliant
        internal IDictionary<string, JToken> Additional;  // Noncompliant
        //                           ^^^^^^

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context) { }
    }

    public sealed class CustomJsonConverter : JsonConverter // Noncompliant
    {
        public override object ReadJson(
            JsonReader reader, // Noncompliant
            Type objectType,
            object existingValue,
            JsonSerializer serializer) // Noncompliant
            => throw new JsonException(); // Noncompliant

        public override void WriteJson(
            JsonWriter writer, // Noncompliant
            object value,
            JsonSerializer serializer) // Noncompliant
            => throw new NotImplementedException();

        public override bool CanConvert(Type objectType) => objectType == typeof(AnnotatedModel);
    }
}

namespace Compliant
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    [JsonObjectCreationHandling(JsonObjectCreationHandling.Populate)]
    [JsonConverter(typeof(CustomJsonConverter))]
    public class AnnotatedModel
    {
        public AnnotatedModel(string value) => Value = value;

        [JsonPropertyName("value")]
        public string Value { get; }

        [JsonIgnore]
        public object Ignore { get; init; }

        [JsonExtensionData]
        internal IDictionary<string, object> Additional;

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context) { }
    }


    public sealed class CustomJsonConverter : JsonConverter<AnnotatedModel>
    {
        public override AnnotatedModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => throw new JsonException();

        public override void Write(Utf8JsonWriter writer, AnnotatedModel value, JsonSerializerOptions options)
           => throw new NotImplementedException();
    }
}
