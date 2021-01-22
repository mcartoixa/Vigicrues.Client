using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Vigicrues
{

    /// <summary>Status of the validation.</summary>
    /// <seealso href="https://www.sandre.eaufrance.fr/?urn=urn:sandre:donnees:390::::::referentiel:3.1:html" />
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum ValidationStatus
    {
        /// <summary>The element has been verified and should not be used.</summary>
        [EnumMember(Value = "Gelé")]
        Frozen,
        /// <summary>The element has been submitted for validation.</summary>
        [EnumMember(Value = "Proposition")]
        Proposed,
        /// <summary>The element is in use but has yet to be validated.</summary>
        [EnumMember(Value = "Provisoire")]
        Temporary,
        /// <summary>The element has been judged as pertinent by a committee of experts.</summary>
        [EnumMember(Value = "Validé")]
        Validated
    }
}
