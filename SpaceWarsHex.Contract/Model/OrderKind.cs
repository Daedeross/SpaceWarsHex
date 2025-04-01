namespace SpaceWars.Model
{
    /// <summary>
    /// Kinds of orders
    /// </summary>
    public enum OrderKind
    {
        /// <summary>
        /// Order to set a reactor's state
        /// </summary>
        ReactorOrder = 0,

        /// <summary>
        /// Order to change entity's Acceleration
        /// </summary>
        MoveOrder = 1,

        /// <summary>
        /// Desired shield strength.
        /// </summary>
        ShieldsOrder = 2,

        /// <summary>
        /// Order for Direct-Fire weapons
        /// </summary>
        DirectFireOder = 2,

        /// <summary>
        /// Order for Beam weapons
        /// </summary>
        BeamOrder = 3,

        /// <summary>
        /// Order for loading and launching Torpedos
        /// </summary>
        TorpedoOrder = 4,

        /// <summary>
        /// Order for firing Bombs
        /// </summary>
        BombOrder = 5,

        /// <summary>
        /// Order for Smoke or Spray
        /// </summary>
        CountermeasureOrder = 6,
    }
}