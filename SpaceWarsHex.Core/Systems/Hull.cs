using SpaceWars.Entities;
using SpaceWars.Interfaces.Prototypes;
using SpaceWars.Interfaces.Systems;
using System;

namespace SpaceWars.Systems
{
    /// <inheritdoc />
    public class Hull : NotificationObject, IHull
    {
        private int _pendingDamage = 0;

        private int m_MaxIntegrity;
        /// <inheritdoc />
        public int MaxIntegrity
        {
            get => m_MaxIntegrity;
            set => this.RaiseAndSetIfChanged(ref m_MaxIntegrity, value);
        }

        private int m_CurrentIntegrity;
        /// <inheritdoc />
        public int CurrentIntegrity
        {
            get => m_CurrentIntegrity;
            set => this.RaiseAndSetIfChanged(ref m_CurrentIntegrity, value);
        }

        /// <summary>
        /// Public constructor for <see cref="Hull"/> component.
        /// </summary>
        /// <param name="prototype"></param>
        public Hull(IHullPrototype prototype)
        {
            m_MaxIntegrity = prototype.MaxIntegrity;
            m_CurrentIntegrity = MaxIntegrity;
        }

        /// <inheritdoc />
        public void ApplyDamage()
        {
            CurrentIntegrity = Math.Max(0, MaxIntegrity - _pendingDamage);
            _pendingDamage = 0;
        }

        /// <inheritdoc />
        public void AssignDamage(int damage)
        {
            _pendingDamage += damage;
        }
    }
}
