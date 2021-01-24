namespace Vigicrues
{

    /// <summary>Station.</summary>
    public class StationEntity:
        Entity
    {

        /// <summary>Creates a new instance of the <see cref="StationEntity" /> type.</summary>
        internal StationEntity() :
            base(EntityType.Station)
        { }

        /// <summary>Creates a new instance of the <see cref="StationEntity" /> type.</summary>
        /// <param name="reference">The unique identifier of the entity.</param>
        public StationEntity(string reference) :
            base(EntityType.Station, reference)
        { }
    }
}
