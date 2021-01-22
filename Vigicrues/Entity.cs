using System;
using System.Text.Json.Serialization;

namespace Vigicrues
{

    /// <summary>Entity for flood vigilance.</summary>
    /// <seealso href="http://id.eaufrance.fr/ddd/VIC/1.1#EntVigiCru" />
    public class Entity
    {
        /// <summary>Identifier of the entity.</summary>
        [JsonPropertyName("@id")]
        public string Identifier { get; internal set; } = string.Empty;
        /// <summary>Unique identifier of the entity.</summary>
        [JsonPropertyName("vic:CdEntVigiCru")]
        public string Reference { get; internal set; } = string.Empty;
        /// <summary>Geographical type of the entity.</summary>
        [JsonPropertyName("vic:TypEntVigiCru")]
        public EntityType EntityType { get; internal set; }
        /// <summary>Designation of the entity.</summary>
        [JsonPropertyName("vic:LbEntVigiCru")]
        public string Designation { get; internal set; } = string.Empty;
        /// <summary>Common acronym of the entity.</summary>
        [JsonPropertyName("vic:AcroEntVigiCru")]
        public string? Acronym { get; internal set; }
        /// <summary>Common acronym of the entity.</summary>
        [JsonPropertyName("vic:GeomEntVigiCru")]
        public string? Geometry { get; internal set; }
        /// <summary>Date of creation of the entity..</summary>
        [JsonPropertyName("vic:DtHrCreatEntVigiCru")]
        public DateTimeOffset? Created { get; internal set; }
        /// <summary>Date of the last update of the entity.</summary>
        [JsonPropertyName("vic:DtHrMajEntVigiCru")]
        public DateTimeOffset? Updated { get; internal set; }
        /// <summary>Status of the entity.</summary>
        [JsonPropertyName("vic:StEntVigiCru")]
        public ValidationStatus? Status { get; internal set; }
    }
}
