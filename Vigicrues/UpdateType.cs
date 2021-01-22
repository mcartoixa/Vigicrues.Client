namespace Vigicrues
{

    /// <summary>Update type.</summary>
    /// <seealso href="http://www.sandre.eaufrance.fr/?urn=urn:sandre:donnees:844::::::referentiel:3.1:html" />
    public enum UpdateType
    {
        /// <summary>Unknown type.</summary>
        Unknown = 0,
        /// <summary>Update was made at the 10h00 nominal hour.</summary>
        NominalHour10 = 1,
        /// <summary>Update was made at the 16h00 nominal hour.</summary>
        NominalHour16 = 2,
        /// <summary>Update was made outside nominal hours.</summary>
        OutsideNominalHours = 3
    }
}
