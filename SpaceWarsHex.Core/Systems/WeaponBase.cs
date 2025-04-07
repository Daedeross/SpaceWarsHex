using SpaceWars.Interfaces.Prototypes;
using SpaceWars.Interfaces.Systems;
using SpaceWars.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWars.Systems
{
    public abstract class WeaponBase : SystemBase, IWeapon
    {
        public FireMode FireMode { get; init; }

        protected WeaponBase(IWeaponPrototype prototype) : base(prototype)
        {
            FireMode = prototype.FireMode;
        }
    }
}
