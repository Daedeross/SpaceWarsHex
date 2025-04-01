namespace SpaceWars.Interfaces.Orders
{
    /// <summary>
    /// Order for an entity to load or unload one of its Torpedoes
    /// </summary>
    public interface ITorpedoLoadOrder : IWeaponOrder
    {
        /// <summary>
        /// The index of the torpedo to load/unload.
        /// </summary>
        int WeaponIndex { get; set; }
    }
}
