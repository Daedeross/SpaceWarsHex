using SpaceWarsHex.Entities;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Interfaces.Systems;
using SpaceWarsHex.Model;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWarsHex.Systems
{
    /// <summary>
    /// Abstract base class for all systems
    /// </summary>
    public abstract class SystemBase : NotificationObject, ISystem
    {
        /// <summary>
        /// The name of the system, for display purposes.
        /// TODO: Figure out if and how to do localization.
        /// </summary>
        private string m_Name = string.Empty;
        public virtual string Name
        {
            get => m_Name;
            set => this.RaiseAndSetIfChanged(ref m_Name, value);
        }

        /// <summary>
        /// Protected, sorted, list of <see cref="DamageThreshold"/>s.
        /// </summary>
        protected readonly List<DamageThreshold> _damageThresholds;

        /// <inheritdoc />
        public IReadOnlyList<DamageThreshold> DamageThresholds => _damageThresholds;

        /// <summary>
        /// Base constructor for all systems.
        /// </summary>
        /// <param name="prototype"><see cref="ISystemPrototype"/></param>
        public SystemBase(ISystemPrototype prototype)
        {
            Name = prototype.Name;
            _damageThresholds = prototype.DamageThresholds.ToList();
            _damageThresholds.Sort();
        }

        /// <inheritdoc />
        public abstract void ApplyDamage(int currentHull, int maxHull);

        /// <inheritdoc />
        public abstract void HandleEndOfTurn(int turnNumber);
    }
}
