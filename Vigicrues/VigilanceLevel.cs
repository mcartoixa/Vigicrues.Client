using System.Text.Json.Serialization;

namespace Vigicrues
{

    /// <summary>Vigilance level.</summary>
    /// <seealso href="http://www.sandre.eaufrance.fr/?urn=urn:sandre:donnees:837::::::referentiel:3.1:html" />
    [JsonConverter(typeof(Serialization.EnumerationJsonConverter<VigilanceLevel>))]
    public enum VigilanceLevel
    {
        /// <summary>No particular vigilance required.</summary>
        Green = 1,
        /// <summary>Some attention is required.</summary>
        Yellow = 2,
        /// <summary>Great vigilance is required.</summary>
        Orange = 3,
        /// <summary>Absolute vigilance is required.</summary>
        Red = 4
    }
}
