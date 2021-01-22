using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vigicrues.Serialization
{

    /// <summary>Converts an enumeration to and from its number representation in a string.</summary>
    /// <remarks>I guess this is what must happen when you go straight from an arcane XML representation to JSON...</remarks>
    /// <typeparam name="TEnum">The type of the enumeration to be converted.</typeparam>
    public class EnumerationJsonConverter<TEnum>:
        JsonConverter<TEnum>
        where TEnum: struct, Enum
    {

        /// <inheritdoc />
        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            TEnum ret = default(TEnum);

            string? svalue = reader.GetString();
            if (svalue != null)
                Enum.TryParse(svalue, out ret);

            return ret;
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("d"));
        }
    }
}
