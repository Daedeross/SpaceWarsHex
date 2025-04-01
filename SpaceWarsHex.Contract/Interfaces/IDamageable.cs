using SpaceWars.Model;

namespace SpaceWars.Interfaces
{
    /// <summary>
    /// Interface for something that can have damage applies to it.
    /// </summary>
    /// <remarks>
    /// It is up the the implementing class to handle the actual affects of damage.
    /// </remarks>
    public interface IDamageable
    {
        /// <summary>
        /// Applies a single instance of damage of a single type.
        /// </summary>
        /// <param name="damageKind">The type of damage being applied. <see cref="DamageKind"/></param>
        /// <param name="damageAmount">The ammout of damage being applied.</param>
        /// <param name="saveShields">True if the owner wishes to keep shields up for a greater energy penalty next turn.</param>
        void ApplyDamage(DamageKind damageKind, int damageAmount, bool saveShields);
    }
}
