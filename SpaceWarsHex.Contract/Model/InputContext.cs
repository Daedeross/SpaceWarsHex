namespace SpaceWarsHex.Model
{
    /// <summary>The current context for user input</summary>
    public enum InputContext
    {
        /// <summary>Nothing selected</summary>
        None,

        /// <summary>Object Selected</summary>
        Normal,

        /// <summary>Giving Move order for selected (Ship or Homing Torpedo)</summary>
        Move,

        /// <summary>Giving DirectFire WeaponOrder for selected</summary>
        DirectFire,

        /// <summary>Giving Beam WeaponOrder for selected</summary>
        Beam,

        /// <summary>Giving BombOrder</summary>
        Bomb,

        /// <summary>Giving Torpedo Fire Order</summary>
        Torpedo,

        /// <summary>Giving Smoke Order</summary>
        Smoke,

        /// <summary>Giving Detonation order for Torepedo</summary>
        Detonate,
    }
}