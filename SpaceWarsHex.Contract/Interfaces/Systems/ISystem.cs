using SpaceWars.Model;
using System.Collections.Generic;
using System.ComponentModel;

namespace SpaceWars.Interfaces.Systems
{
    /// <summary>
    /// Base interface for all systems.
    /// </summary>
    public interface ISystem : INotifyPropertyChanged
    {
        /// <summary>
        /// The name of the system.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// <see cref="DamageThreshold"/>
        /// </summary>
        IReadOnlyList<DamageThreshold> DamageThresholds { get; }

        /// <summary>
        /// Have the system calculate the effects of damage.
        /// </summary>
        /// <param name="currentHull">The current hull strength of the entity</param>
        /// <param name="maxHull">The maximum hull strength of the entity</param>
        void ApplyDamage(int currentHull, int maxHull);

        /// <summary>
        /// Handles any end of turn book keeping needed by the system.
        /// </summary>
        /// <param name="turnNumber"></param>
        void HandleEndOfTurn(int turnNumber);
    }
}
