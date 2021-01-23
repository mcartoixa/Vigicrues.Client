using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vigicrues.Serialization
{

    /// <summary>Converts an <see cref="Entity" /> to and from a JSON representation.</summary>
    public class EntityJsonConverter:
        JsonConverter<Entity>
    {

        internal class JsonEntity:
            Entity
        {

            public JsonEntity():
                base(0)
            { }

            [JsonPropertyName("vic:TypEntVigiCru")]
            public new EntityType EntityType { get; set; }
        }

        /// <inheritdoc />
        public override Entity? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Entity ret;

            // Functionality over performance here
            JsonEntity? entity = JsonSerializer.Deserialize<JsonEntity>(ref reader, options);
            if (entity == null)
                return null;

            switch (entity.EntityType)
            {
            case EntityType.County:
                ret = new CountyEntity();
                break;
            case EntityType.Local:
                ret = new LocalEntity();
                break;
            case EntityType.MetropolitanArea:
                ret = new MetropolitanAreaEntity();
                break;
            case EntityType.OverseasTerritory:
                ret = new OverseasTerritoryEntity();
                break;
            case EntityType.Section:
                ret = new SectionEntity();
                break;
            case EntityType.Station:
                ret = new StationEntity();
                break;
            case EntityType.Territory:
                ret = new TerritoryEntity();
                break;
            case EntityType.Watershed:
                ret = new WatershedEntity();
                break;
            default:
                throw new JsonException();
            }

            ret.Acronym = entity.Acronym;
            ret.Children = entity.Children;
            ret.Created = entity.Created;
            ret.Designation = entity.Designation;
            ret.Geometry = entity.Geometry;
            ret.Identifier = entity.Identifier;
            ret.Informations = entity.Informations;
            ret.IsDownstreamFrom = entity.IsDownstreamFrom;
            ret.IsUpstreamFrom = entity.IsUpstreamFrom;
            ret.Parents = entity.Parents;
            ret.Reference = entity.Reference;
            ret.Status = entity.Status;
            ret.Updated = entity.Updated;

            return ret;
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, Entity value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
