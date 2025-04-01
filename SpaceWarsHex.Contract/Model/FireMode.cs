namespace SpaceWars.Model
{
    /// <summary>
    /// The differnt modes for fireing a weapon or countermeasure.
    /// </summary>
    public enum FireMode
    {
        /// <summary>Targets a single entity and fired instantaneously.</summary>
        DirectFire = 0,

        /// <summary>Fired as a cone in one of 12 directions.</summary>
        Beam = 1,

        /// <summary>Targets a specific hex some number of turns in the future.</summary>
        Bomb = 2,

        /// <summary>Launched out as a new single-hex entity with a velocity.</summary>
        Torpedo = 3,

        /// <summary>Launched out as a multi-hex entity with a velocity, a "main" hex, and orientation</summary>
        Wall = 4,

        /// <summary>Fired as a dispipating ring-shaped pulse from the ship's current position.</summary>
        Pulse = 5,

        /// <summary>Fired as a sustained ring-shaped pulse from the ship's current position.</summary>
        Wave = 6,
    }
}
