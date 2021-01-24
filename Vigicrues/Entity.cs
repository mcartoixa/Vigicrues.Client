using System;
using System.Text.Json.Serialization;

namespace Vigicrues
{

    /// <summary>Entity for flood vigilance.</summary>
    /// <seealso href="http://id.eaufrance.fr/ddd/VIC/1.1/EntVigiCru" />
    [JsonConverter(typeof(Serialization.EntityJsonConverter))]
    public abstract class Entity:
        IVigicrueObject
    {

        /// <summary>Creates a new instance of the <see cref="Entity" /> type.</summary>
        /// <param name="type">The geographical type of the entity.</param>
        /// <param name="reference">The unique identifier of the entity.</param>
        protected Entity(EntityType type, string reference):
            this(type)
        {
            Reference = reference;
        }

        /// <summary>Creates a new instance of the <see cref="Entity" /> type.</summary>
        /// <param name="type">The geographical type of the entity.</param>
        protected Entity(EntityType type)
        {
            EntityType = type;
        }

        private Entity()
        { }

        /// <summary>Identifier of the entity.</summary>
        [JsonPropertyName("@id")]
        public string Identifier { get; set; } = string.Empty;
        /// <summary>Unique identifier of the entity.</summary>
        [JsonPropertyName("vic:CdEntVigiCru")]
        public string Reference { get; set; } = string.Empty;
        /// <summary>Geographical type of the entity.</summary>
        [JsonPropertyName("vic:TypEntVigiCru")]
        public EntityType EntityType { get; private set; }
        /// <summary>Designation of the entity.</summary>
        [JsonPropertyName("vic:LbEntVigiCru")]
        public string Designation { get; set; } = string.Empty;
        /// <summary>Common acronym of the entity.</summary>
        [JsonPropertyName("vic:AcroEntVigiCru")]
        public string? Acronym { get; set; }
        /// <summary>Common acronym of the entity.</summary>
        [JsonPropertyName("vic:GeomEntVigiCru")]
        public string? Geometry { get; set; }
        /// <summary>Date of creation of the entity..</summary>
        [JsonPropertyName("vic:DtHrCreatEntVigiCru")]
        public DateTimeOffset? Created { get; set; }
        /// <summary>Date of the last update of the entity.</summary>
        [JsonPropertyName("vic:DtHrMajEntVigiCru")]
        public DateTimeOffset? Updated { get; set; }
        /// <summary>Status of the entity.</summary>
        [JsonPropertyName("vic:StEntVigiCru")]
        public ValidationStatus? Status { get; set; }
        /// <summary>Entities that are upstream from the current one.</summary>
        [JsonPropertyName("vic:estEnAval")]
        public Entity[] IsDownstreamFrom { get; set; } = new Entity[0];
        /// <summary>Children entities.</summary>
        [JsonPropertyName("vic:aNMoinsUn")]
        public Entity[] Children { get; set; } = new Entity[0];
        /// <summary>Information related to the current entity.</summary>
        [JsonPropertyName("vic:infoDeVigilanceDeLEntite")]
        public Information[] Informations { get; set; } = new Information[0];
        /// <summary>Entities that are downstream from the current one.</summary>
        [JsonPropertyName("vic:estEnAmont")]
        public Entity[] IsUpstreamFrom { get; set; } = new Entity[0];
        /// <summary>Parent entity.</summary>
        [JsonPropertyName("vic:aNPlusUn")]
        public Entity? Parent { get; set; }
    }
}
