using System;
using System.Text.Json.Serialization;

namespace Vigicrues
{

    /// <summary>Information about the situation an <see cref="Entity" /> is in regarding flood vigilance.</summary>
    /// <seealso href="http://id.eaufrance.fr/ddd/VIC/1.1#InfoVigiCru" />
    public class Information
    {

        /// <summary>Identifier of the information.</summary>
        [JsonPropertyName("vic:RefInfoVigiCru")]
        public string Reference { get; internal set; } = string.Empty;
        /// <summary>Date when the information was produced.</summary>
        [JsonPropertyName("vic:DtHrInfoVigiCru")]
        public DateTimeOffset Produced { get; internal set; }
        /// <summary>Type of update for this information.</summary>
        [JsonPropertyName("vic:TypInfoVigiCru")]
        public UpdateType UpdateType { get; internal set; }
        /// <summary>Indicative date of when the next update should be produced.</summary>
        [JsonPropertyName("vic:DtHrSuivInfoVigiCru")]
        public DateTimeOffset? NextUpdate { get; internal set; }
        /// <summary>Vigilance level related to this information.</summary>
        [JsonPropertyName("vic:NivInfoVigiCru")]
        public VigilanceLevel VigilanceLevel { get; internal set; }
        /// <summary>URL to the pictogram representing the <see cref="VigilanceLevel" />.</summary>
        [JsonPropertyName("vic:URLPictoInfoVigiCru")]
        public string? VigilanceLevelPictogramUrl { get; internal set; }
        /// <summary>Indicates whether the vigilance level is calculated or not.</summary>
        [JsonPropertyName("vic:EstNivCalInfoVigiCru")]
        public bool? IsVigilanceLevelCalculated { get; internal set; }
        /// <summary>Probable recurrence of the current hazard.</summary>
        [JsonPropertyName("vic:PeriodInfoVigiCru")]
        public HazardRecurrence? Recurrence { get; internal set; }
        /// <summary>Textual qualification of the current hazard.</summary>
        [JsonPropertyName("vic:QualifInfoVigiCru")]
        public string Qualification { get; internal set; } = string.Empty;
        /// <summary>New facts since the last update.</summary>
        [JsonPropertyName("vic:FaitNouvInfoVigiCru")]
        public string? News { get; internal set; }
        /// <summary>Advice to the population affected by the current hazard.</summary>
        [JsonPropertyName("vic:ConsInfoVigiCru")]
        public string? Advice { get; internal set; }
        /// <summary>Possible consequences of the current situation.</summary>
        [JsonPropertyName("vic:ConseqInfoVigiCru")]
        public string? PossibleConsequences { get; internal set; }
        /// <summary>Characterizes the validity of this information.</summary>
        [JsonPropertyName("vic:StInfoVigiCru")]
        public InformationStatus? Status { get; internal set; }
        /// <summary>List of sections affected by the current situation.</summary>
        [JsonPropertyName("vic:ListeTronInfoVigiCru")]
        public string? AffectedSections { get; internal set; }
        /// <summary>Textual summary of the current situation in meteorological and hydrological terms.</summary>
        [JsonPropertyName("vic:SituActuInfoVigiCru")]
        public string? CurrentSituation { get; internal set; }
        /// <summary>Forecast of the evolution of the current situation.</summary>
        [JsonPropertyName("vic:EvolInfoVigiCru")]
        public string? ForecastSituation { get; internal set; }
        /// <summary>Hydrological situation.</summary>
        [JsonPropertyName("vic:SituHydroInfoVigiCru")]
        public string? HydrologicSituation { get; internal set; }
        /// <summary>The previous related <see cref="Information" />.</summary>
        [JsonPropertyName("vic:succedeALInfo")]
        public Information? Follows { get; internal set; }
        /// <summary>List of the <see cref="Information" /> that have been synthesized in this one.</summary>
        [JsonPropertyName("vic:estLaSyntheseDe")]
        public Information[]? SynthesisOf { get; internal set; }
        /// <summary>The following related <see cref="Information" />.</summary>
        [JsonPropertyName("vic:precedeLInfo")]
        public Information? ComesBefore { get; internal set; }
        /// <summary>The <see cref="Information" /> that synthesizes the current one.</summary>
        [JsonPropertyName("vic:aPourSynthese")]
        public Information? IsSynthesizedBy { get; internal set; }
        /// <summary>The <see cref="Entity" /> affected by this information.</summary>
        [JsonPropertyName("vic:porteSurLEntite")]
        public Entity? Entity { get; internal set; }
    }
}
