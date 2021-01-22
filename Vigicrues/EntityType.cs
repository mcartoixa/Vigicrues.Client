using System.Text.Json.Serialization;

namespace Vigicrues
{

    /// <summary>Geographical characterization of an entity.</summary>
    /// <seealso href="http://www.sandre.eaufrance.fr/?urn=urn:sandre:donnees:849::::::referentiel:3.1:html" />
    [JsonConverter(typeof(Serialization.EnumerationJsonConverter<EntityType>))]
    public enum EntityType
    {

        /// <summary>Local entity.</summary>
        Local = 1,
        /// <summary>Metropolitan area.</summary>
        MetropolitanArea = 2,
        /// <summary>Overseas territories.</summary>
        OverseasTerritories = 3,
        /// <summary>County.</summary>
        County = 4,
        /// <summary>Territory.</summary>
        Territory = 5,
        /// <summary>Watershed.</summary>
        Watershed = 6,
        /// <summary>Station.</summary>
        Station = 7,
        /// <summary>Section.</summary>
        Section = 8
    }
}
