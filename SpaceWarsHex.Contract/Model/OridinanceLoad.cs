using SpaceWarsHex.Interfaces.Systems;

namespace SpaceWarsHex.Model
{
    /// <summary>
    /// Enum of ordering ordinance to load/unload or fire.
    /// </summary>
    public enum OridinanceLoad
    {
        /// <summary>Order the ordinance to load. Will be ready to fire next turn.</summary>
        Load = 0,
        /// <summary>Order the ordinance to unload.</summary>
        Unload = 1,
        /// <summary>
        /// Order the ordinance to fire.
        /// Only valid if Loaded last turn or <see cref="ITorpedoLauncher.LoadFire"/> is true.
        /// </summary>
        Fire = 2,
    }
}
