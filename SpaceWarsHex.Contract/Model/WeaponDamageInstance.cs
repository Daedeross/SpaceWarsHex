using System.Collections.Generic;

namespace SpaceWarsHex.Model
{
    /// <summary>
    /// Represents the damage done by a single weapon at once to a single target.
    /// </summary>
    public class WeaponDamageInstance
    {
        /// <summary>
        /// All the damge instances, usually their is only one or two.
        /// </summary>
        public List<DamageInstance> DamageInstances { get; set; } = [];
    }
}
