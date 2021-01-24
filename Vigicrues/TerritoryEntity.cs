namespace Vigicrues
{

    /// <summary>Territory.</summary>
    public class TerritoryEntity:
        Entity
    {

        /// <summary>Creates a new instance of the <see cref="TerritoryEntity" /> type.</summary>
        internal TerritoryEntity():
            base(EntityType.Territory)
        { }

        /// <summary>Creates a new instance of the <see cref="TerritoryEntity" /> type.</summary>
        /// <param name="reference">The unique identifier of the entity.</param>
        public TerritoryEntity(string reference):
            base(EntityType.Territory, reference)
        { }
    }
}
