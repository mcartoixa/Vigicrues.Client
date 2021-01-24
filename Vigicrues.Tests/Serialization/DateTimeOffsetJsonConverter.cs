using System;
using System.Text.Json;
using Xunit;

namespace Vigicrues.Serialization
{
    public class DateTimeOffsetJsonConverterTests
    {

        [Theory]
        [InlineData("\"2021-01-23T13:58:40+0100\"")]
        [InlineData("\"2021-01-24T10:00:00+0100\"")]
        public void Read_ShouldParseDateTimeOffsetValues(string input)
        {
            DateTimeOffset? value = JsonSerializer.Deserialize<DateTimeOffset?>(input, _JsonSerializerOptions);

            Assert.NotNull(value);
        }

        [Fact]
        public void Read_ShouldParseEmptyStringAsNull()
        {
            string input = "\"\"";
            DateTimeOffset? value = JsonSerializer.Deserialize<DateTimeOffset?>(input, _JsonSerializerOptions);

            Assert.Null(value);
        }

        private JsonSerializerOptions _JsonSerializerOptions = new JsonSerializerOptions {
            Converters = {
                new DateTimeOffsetJsonConverter()
            }
        };

    }
}
