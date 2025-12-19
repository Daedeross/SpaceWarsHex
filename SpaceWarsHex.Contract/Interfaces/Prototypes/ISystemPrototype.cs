using SpaceWarsHex.Model;
using System;
using System.Collections.Generic;

namespace SpaceWarsHex.Interfaces.Prototypes
{
    /// <summary>
    /// Root interface for all system prototypes.
    /// </summary>
    /// <remarks>
    /// A 'Prototype' should contain all the data which defines a system at the start of an encounter.
    /// Think of a prototype as the equivalent to the text and numbers that would define a system
    /// on the ship's sheet for the tabletop game.
    /// </remarks>
    public interface ISystemPrototype: IPrototype, IHaveId, ICloneable
    {
        /// <summary>
        /// The name of the system.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// <see cref="DamageThreshold"/>
        /// </summary>
        IEnumerable<DamageThreshold> DamageThresholds { get; }
    }
}
