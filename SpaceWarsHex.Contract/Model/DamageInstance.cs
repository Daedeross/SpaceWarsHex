namespace SpaceWarsHex.Model
{
    /// <summary>
    /// Represents a singe insance of damage, after dice are rolled and such.
    /// </summary>
    public class DamageInstance
    {
        /// <summary>
        /// The type of damage.
        /// </summary>
        public DamageKind DamageKind { get; init; }

        /// <summary>
        /// The ammount of damage in this insance.
        /// </summary>
        public int DamageValue { get; init; }
    }
}
