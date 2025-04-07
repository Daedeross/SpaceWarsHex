namespace SpaceWarsHex.Model
{
    /// <summary>
    /// The kinds of damage that can be applied to <see cref="SpaceWarsHex.Interfaces.IDamageable"/>
    /// </summary>
    public enum DamageKind
    {
        /// <summary>
        /// Energy/Phasor damage.
        /// </summary>
        Energy = 0,
        /// <summary>
        /// Physical/Concussion damage.
        /// </summary>
        Physical = 1,
        /// <summary>
        /// Direct Damage to hull, ignores shields.
        /// </summary>
        Direct,
        /// <summary>
        /// Zap damage. Reduces target's acceleration.
        /// </summary>
        Zap = 2,
    }
}
