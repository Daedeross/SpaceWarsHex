using System;
using System.Collections.Generic;

namespace SpaceWars.Interfaces.Prototypes
{
    /// <summary>
    /// Interface for ship prototypes.
    /// </summary>
    public interface IShipPrototype : ISingleHexObjectPrototype
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        IReactorPrototype Reactor { get; }
        IDrivePrototype Drive { get; }
        IShieldsPrototype Shields { get; }
        IHullPrototype Hull { get; }
        IReadOnlyList<IEnergyWeaponPrototype> EnergyWeapons { get; }
        IReadOnlyList<IOrdinancePrototype> Ordinances { get; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
