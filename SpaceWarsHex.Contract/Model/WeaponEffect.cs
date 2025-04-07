namespace SpaceWarsHex.Model
{
    /// <summary>
    /// DTO class represinging an effect of a weapon.
    /// </summary>
    public class WeaponEffect
    {
        /// <summary>
        /// The type of damage.
        /// </summary>
        public DamageKind DamageKind { get; init; }

        /// <summary>
        /// The ammount of damage per hit.
        /// </summary>
        public int DamageValue { get; init; }
    }
}
