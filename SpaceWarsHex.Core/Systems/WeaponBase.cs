using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Interfaces.Systems;
using SpaceWarsHex.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWarsHex.Systems
{
    public abstract class WeaponBase : SystemBase, IWeapon
    {

        /// <inheritdoc />
        public FireMode FireMode { get; private set; }

        /// <inheritdoc />
        public TurnPhase FirePhase { get; private set; }

        protected WeaponBase(IWeaponPrototype prototype) : base(prototype)
        {
            FireMode = prototype.FireMode;
            FirePhase = prototype.FirePhase;
        }
    }
}
