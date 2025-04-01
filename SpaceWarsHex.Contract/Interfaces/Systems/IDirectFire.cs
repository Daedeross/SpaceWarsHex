namespace SpaceWars.Interfaces.Systems
{
    /// <summary>
    /// Interface for a Direct-Fire weapon.
    /// </summary>
    public interface IDirectFire : IWeapon
    {
        /// <summary>
        /// The max range of the weapon. Null = infinite.
        /// </summary>
        int? MaxRange { get; }
    }
}
