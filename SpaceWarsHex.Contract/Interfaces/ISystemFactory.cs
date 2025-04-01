using SpaceWars.Interfaces.Prototypes;
using SpaceWars.Interfaces.Systems;

namespace SpaceWars.Interfaces
{
    /// <summary>
    /// Interface for <see cref="ISystem"/> creating factory.
    /// </summary>
    public interface ISystemFactory
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IDrive Create(IDrivePrototype input);

        public IEnergyWeapon Create(IEnergyWeaponPrototype input);

        public IOrdinance Create(IOrdinancePrototype input);

        public IReactor Create(IReactorPrototype input);

        public IShields Create(IShieldsPrototype input);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
