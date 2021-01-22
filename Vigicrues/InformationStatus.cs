using System.Text.Json.Serialization;

namespace Vigicrues
{

    /// <summary>Status of the information.</summary>
    /// <seealso href="http://www.sandre.eaufrance.fr/?urn=urn:sandre:donnees:848::::::referentiel:3.1:html" />
    [JsonConverter(typeof(Serialization.EnumerationJsonConverter<InformationStatus>))]
    public enum InformationStatus
    {
        /// <summary>Unknown status.</summary>
        Unknown = 0,
        /// <summary>The information is actively being redacted.</summary>
        InProgress = 1,
        /// <summary>The information is being finalized.</summary>
        Proposed = 2,
        /// <summary>The information has been finalized and should not be further updated.</summary>
        Produced = 3,
        /// <summary>The information is active and has been largely broadcast.</summary>
        Active = 4,
        /// <summary>The information has been superseeded.</summary>
        Obsolete = 5
    }
}
