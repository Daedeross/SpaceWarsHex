using SpaceWars.Model;

namespace SpaceWars.Interfaces.Prototypes
{
    /// <summary>
    /// Base prototype interface for all weapons.
    /// </summary>
    public interface IWeaponPrototype: ISystemPrototype
    {
        /// <summary>
        /// <see cref="FireMode"/>
        /// </summary>
        FireMode FireMode { get; set; }
    }
}
