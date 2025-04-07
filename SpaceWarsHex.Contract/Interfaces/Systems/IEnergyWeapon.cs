using SpaceWarsHex.Model;
using System.Collections.Generic;

namespace SpaceWarsHex.Interfaces.Systems
{
    /// <summary>
    /// Inteface for weapons that require input energy to function.
    /// </summary>
    public interface IEnergyWeapon : IWeapon
    {
        /// <summary>
        /// The initial maximum number of effect dice the system can output.
        /// </summary>
        int InitialMaxDice { get; }

        /// <summary>
        /// The current maximum number of effect dice the system can output.
        /// </summary>
        int CurrentMaxDice { get; }

        /// <summary>
        /// True if the weapon is affected by smoke and the like.
        /// </summary>
        bool Visual { get; }

        /// <summary>
        /// The ammount of energy required to get one die of effect.
        /// </summary>
        int EnergyPerDie { get; }

        /// <summary>
        /// The ordered list of effects that one "hit" will cause.
        /// </summary>
        /// <remarks>
        /// Reminder: effects are totaled up for the entire weapon before applying.
        /// The order is important.
        /// </remarks>
        IReadOnlyList<WeaponEffect> Effects { get; }
    }
}
