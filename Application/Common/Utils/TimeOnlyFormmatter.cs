using System.Text.Json;
using System.Text.Json.Serialization;

namespace ColegioMozart.Application.Common.Utils
{
    public class TimeOnlyFormmatter : JsonConverter<TimeOnly>
    {
        private readonly string serializationFormat;

        public TimeOnlyFormmatter() : this(null)
        {
        }

        public TimeOnlyFormmatter(string? serializationFormat)
        {
            this.serializationFormat = serializationFormat ?? "hh:mm tt";
        }

        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return TimeOnly.Parse(value!);
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value,
                                            JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(serializationFormat));
    }
}
