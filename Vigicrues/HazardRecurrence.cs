using System.Text.Json.Serialization;

namespace Vigicrues
{

    /// <summary>Hazard recurrence.</summary>
    /// <seealso href="http://www.sandre.eaufrance.fr/?urn=urn:sandre:donnees:894::::::referentiel:3.1:html" />
    [JsonConverter(typeof(Serialization.EnumerationJsonConverter<HazardRecurrence>))]
    public enum HazardRecurrence
    {
        /// <summary>Unknown recurrence.</summary>
        Unknown = 0,
        /// <summary>Hazard that occurs about every 100 years.</summary>
        Centennial = 1,
        /// <summary>Hazard that occurs about every 10,000 years.</summary>
        Decamillennial = 2,
        /// <summary>Hazard that occurs about every 1,000 years.</summary>
        Millennial = 3,
        /// <summary>Hazard that occurs about every 300 years.</summary>
        Tricentennial = 4,
        /// <summary>Hazard that occurs about every 200 years.</summary>
        Bicentennial = 5,
        /// <summary>Hazard that occurs about every 50 years.</summary>
        Semicentennial = 6,
        /// <summary>Hazard that occurs about every 30 years.</summary>
        Tridecennial = 7,
        /// <summary>Hazard that occurs about every 20 years.</summary>
        Vicennial = 8,
        /// <summary>Hazard that occurs about every 10 years.</summary>
        Decennial = 9,
        /// <summary>Hazard that occurs about every 5 years.</summary>
        Quinquennial = 10,
        /// <summary>Hazard that occurs about every 3 years.</summary>
        Triennial = 11,
        /// <summary>Hazard that occurs about every 2 years.</summary>
        Biennial = 12,
        /// <summary>Hazard that occurs about every year.</summary>
        Annual = 13,
        /// <summary>Hazard that occurs about every 6 months.</summary>
        Biannual = 14
    }
}
