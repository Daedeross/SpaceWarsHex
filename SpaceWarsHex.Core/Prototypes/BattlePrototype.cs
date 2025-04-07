using SpaceWarsHex.Interfaces.Prototypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceWarsHex.Prototypes
{
    /// <summary>
    /// Serializable class for saving and loading of space battles.
    /// </summary>
    public class BattlePrototype : IBattlePrototype
    {
        /// <inheritdoc/>
        public Guid Id { get; set; }
    }
}
