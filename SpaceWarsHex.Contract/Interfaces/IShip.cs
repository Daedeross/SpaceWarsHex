using SpaceWars.Interfaces.Orders;
using SpaceWars.Interfaces.Prototypes;
using SpaceWars.Interfaces.Rules;
using SpaceWars.Interfaces.Systems;
using SpaceWars.Model;
using System.Collections.Generic;

namespace SpaceWars.Interfaces
{
    /// <summary>
    /// Interface for ships
    /// </summary>
    public interface IShip : IMovingHexObject, IOrderable, IDamageable, IOrderable<IReactorOrder>, IOrderable<IMoveOrder>, IOrderable<IShieldOrder>, ISelectable, ITargetable
    {
        /// <summary>
        /// The ship's <see cref="IReactor"/>
        /// </summary>
        public IReactor Reactor { get; }

        /// <summary>
        /// The ship's Warp <see cref="IDrive"/>
        /// </summary>
        public IDrive Drive { get; }

        /// <summary>
        /// <see cref="IShields"/>
        /// </summary>
        public IShields Shields { get; }

        /// <summary>
        /// The "HP" of the ship
        /// </summary>
        public IHull Hull { get; }

        /// <summary>
        /// The ship's energy weapons. Order is important because weapon orders specify the weapon via a numeric index.
        /// </summary>
        public IReadOnlyList<IEnergyWeapon> EnergyWeapons { get; }

        /// <summary>
        /// The current energy weapon order, if any.
        /// </summary>
        public IEnergyWeaponOrder? CurrentEnergyWeaponOrder { get; }

        /// <summary>
        /// The ships ordinance. Order is important because weapon orders specify the weapon via a numeric index.
        /// </summary>
        public IReadOnlyList<IOrdinance> Ordinances { get; }

        /// <summary>
        /// DI endpoint since we can't call the constructor directly.
        /// </summary>
        /// <param name="shipPrototype">The prototype defining the ship and its systems.</param>
        /// <param name="rules"><see cref="IRules"/></param>
        /// <returns>this</returns>
        //public IShip Initialize(IShipPrototype shipPrototype, IRules rules);
    }
}
