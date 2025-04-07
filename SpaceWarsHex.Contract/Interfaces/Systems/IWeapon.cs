using SpaceWars.Model;

namespace SpaceWars.Interfaces.Systems
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
    }
}
