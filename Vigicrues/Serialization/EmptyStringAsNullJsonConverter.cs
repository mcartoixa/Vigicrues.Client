using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vigicrues.Serialization
{

    /// <summary>Converts empty strings to the null value equivalent for the specified <typeparamref name="T">type</typeparamref>.</summary>
    /// <typeparam name="T">The type to convert.</typeparam>
    public class EmptyStringAsNullJsonConverter<T>:
        JsonConverter<T>
        where T:
            struct
    {

        /// <inheritdoc />
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                if (string.IsNullOrEmpty(value))
                    return default(T);
                else
                    return JsonSerializer.Deserialize<T>(value, options);
            } else
                return JsonSerializer.Deserialize<T>(ref reader, options);
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
