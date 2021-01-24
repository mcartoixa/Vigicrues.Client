namespace Vigicrues
{

    /// <summary>Section.</summary>
    public class SectionEntity:
        Entity
    {

        /// <summary>Creates a new instance of the <see cref="SectionEntity" /> type.</summary>
        internal SectionEntity() :
            base(EntityType.Section)
        { }

        /// <summary>Creates a new instance of the <see cref="SectionEntity" /> type.</summary>
        /// <param name="reference">The unique identifier of the entity.</param>
        public SectionEntity(string reference) :
            base(EntityType.Section, reference)
        { }
    }
}
