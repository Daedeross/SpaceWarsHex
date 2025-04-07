using SpaceWarsHex.Model;

namespace SpaceWarsHex.Interfaces.Prototypes
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

        /// <summary>
        /// The phase the weapon is fired/launched. Only <see cref="TurnPhase.Weapons1"/> and <see cref="TurnPhase.Weapons2"/> are valid
        /// </summary>
        TurnPhase FirePhase { get; }
    }
}
