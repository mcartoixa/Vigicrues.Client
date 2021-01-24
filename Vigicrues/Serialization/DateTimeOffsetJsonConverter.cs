using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vigicrues.Serialization
{

    /// <summary>Converts <see cref="DateTimeOffset" /> to and from JSON.</summary>
    /// <remarks>Oddly enough, null value are serialized as empty strings.</remarks>
    public class DateTimeOffsetJsonConverter:
        JsonConverter<DateTimeOffset?>
    {

        /// <inheritdoc />
        public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                if (!string.IsNullOrEmpty(value))
                    return DateTimeOffset.Parse(value);
                else
                    return null;
            }

            return null;
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
                writer.WriteStringValue(value.Value);
            else
                writer.WriteStringValue(string.Empty);
        }
    }
}
