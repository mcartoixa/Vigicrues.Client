namespace Vigicrues
{

    /// <summary>Watershed.</summary>
    /// <seealso href="http://id.eaufrance.fr/ddd/VIC/1.1/BassinEntVigiCru" />
    public class WatershedEntity:
        Entity
    {

        /// <summary>Creates a new instance of the <see cref="WatershedEntity" /> type.</summary>
        public WatershedEntity():
            base(EntityType.Watershed)
        { }
    }
}
