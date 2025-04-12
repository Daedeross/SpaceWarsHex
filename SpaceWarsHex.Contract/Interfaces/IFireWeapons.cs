using SpaceWarsHex.Interfaces.Orders;
using SpaceWarsHex.Interfaces.Systems;
using SpaceWarsHex.Model;
using System.Collections.Generic;

namespace SpaceWarsHex.Interfaces
{
    public interface IFireWeapons
    {
        IReadOnlyCollection<(IWeapon, IWeaponOrder)> GetFiringWeapons(TurnPhase turnPhase);
    }
}
