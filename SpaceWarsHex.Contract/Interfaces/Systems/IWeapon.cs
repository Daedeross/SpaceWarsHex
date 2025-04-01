using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWars.Interfaces.Systems
{
    /// <summary>
    /// Base type for <see cref="IOrdinance"/> and <see cref="IEnergyWeapon"/>.
    /// </summary>
    /// <remarks>
    /// Not necessarily a weapon, per se. Could be a countermeasure, for example.
    /// </remarks>
    public interface IWeapon : ISystem
    {
    }
}
