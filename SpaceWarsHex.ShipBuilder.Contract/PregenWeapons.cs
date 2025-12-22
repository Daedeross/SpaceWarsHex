using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Interfaces.Prototypes;
using System.Collections.Generic;

namespace SpaceWarsHex.ShipBuilder
{
    public interface IPregenWeapons : IPrototype, IHaveId
    {
        public IReadOnlyList<IEnergyWeaponPrototype> EnergyWeapons { get; }
    }
}
