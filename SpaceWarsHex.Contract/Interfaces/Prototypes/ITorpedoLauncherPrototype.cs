namespace SpaceWars.Interfaces.Prototypes
{
    /// <summary>
    /// Prototype interface for all systems that launch torpedoes.
    /// </summary>
    public interface ITorpedoLauncherPrototype : ISystemPrototype
    {
        /// <summary>
        /// The minimum warp the torpedo must be launched at.
        /// </summary>
        int MinWarp { get; }

        /// <summary>
        /// The maximum warp the torpedo may be launched at.
        /// </summary>
        int MaxWarp { get; }

        /// <summary>
        /// True if the torpedos are homing-capable
        /// </summary>
        bool Homing { get; }

        /// <summary>
        /// True if the tube can load and fire in the same turn.
        /// Effectively, it can fire without loading first.
        /// </summary>
        bool LoadFire { get; }
    }
}
