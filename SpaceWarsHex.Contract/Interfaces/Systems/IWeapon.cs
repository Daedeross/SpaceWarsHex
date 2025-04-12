using SpaceWarsHex.Model;

namespace SpaceWarsHex.Interfaces.Systems
{
    /// <summary>
    /// Base type for <see cref="IOrdinance"/> and <see cref="IEnergyWeapon"/>.
    /// </summary>
    /// <remarks>
    /// Not necessarily a weapon, per se. Could be a countermeasure, for example.
    /// </remarks>
    public interface IWeapon : ISystem
    {
        /// <summary>
        /// <see cref="FireMode"/>
        /// </summary>
        FireMode FireMode { get; }


        /// <summary>
        /// The phase the weapon is fired/launched. Only <see cref="TurnPhase.Weapons1"/> and <see cref="TurnPhase.Weapons2"/> are valid
        /// </summary>
        TurnPhase FirePhase { get; }

        /// <summary>
        /// The max range of the weapon. Non-Positive = infinite.
        /// </summary>
        int MaxRange { get; }
    }
}
