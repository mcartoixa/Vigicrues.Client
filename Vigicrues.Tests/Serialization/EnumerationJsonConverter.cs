using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace Vigicrues.Serialization
{

    public class EnumerationJsonConverterTests
    {

        [JsonConverter(typeof(EnumerationJsonConverter<TestEnum>))]
        public enum TestEnum
        {
            Value1 = 0,
            Value2 = 1,
            Value3 = 99
        }

        [Theory]
        [InlineData("\"0\"", TestEnum.Value1)]
        [InlineData("\"1\"", TestEnum.Value2)]
        [InlineData("\"99\"", TestEnum.Value3)]
        public void Read_ShouldParseIntegerValuesInStrings(string input, TestEnum expected)
        {
            TestEnum value = JsonSerializer.Deserialize<TestEnum>(input);

            Assert.Equal(value, expected);
        }

        [Theory]
        [InlineData(TestEnum.Value1, "\"0\"")]
        [InlineData(TestEnum.Value2, "\"1\"")]
        [InlineData(TestEnum.Value3, "\"99\"")]
        public void Write_ShouldOutputIntegerValuesInStrings(TestEnum input, string expected)
        {
            string value = JsonSerializer.Serialize(input);

            Assert.Equal(value, expected);
        }
    }
}
