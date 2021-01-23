using System;
using System.Text.Json;
using Xunit;

namespace Vigicrues.Serialization
{
    public class EntityJsonConverterTests
    {

        [Theory]
        [InlineData("{\"vic:TypEntVigiCru\":\"1\"}", typeof(LocalEntity))]
        [InlineData("{\"vic:TypEntVigiCru\":\"2\"}", typeof(MetropolitanAreaEntity))]
        [InlineData("{\"vic:TypEntVigiCru\":\"3\"}", typeof(OverseasTerritoryEntity))]
        [InlineData("{\"vic:TypEntVigiCru\":\"4\"}", typeof(CountyEntity))]
        [InlineData("{\"vic:TypEntVigiCru\":\"5\"}", typeof(TerritoryEntity))]
        [InlineData("{\"vic:TypEntVigiCru\":\"6\"}", typeof(WatershedEntity))]
        [InlineData("{\"vic:TypEntVigiCru\":\"7\"}", typeof(StationEntity))]
        [InlineData("{\"vic:TypEntVigiCru\":\"8\"}", typeof(SectionEntity))]
        public void Read_ShouldParseIntegerValuesInStrings(string input, Type expectedType)
        {
            Entity entity = JsonSerializer.Deserialize<Entity>(input);

            Assert.IsType(expectedType, entity);
        }
    }
}
