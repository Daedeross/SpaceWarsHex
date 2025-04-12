using SpaceWarsHex.Entities;
using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Interfaces.Systems;
using SpaceWarsHex.States.Systems;

namespace SpaceWarsHex.Systems
{
    /// <inheritdoc />
    public class Hull : NotificationObject, IHull, IHaveState<HullState>
    {
        private HullState _state;

        private int m_MaxIntegrity;
        /// <inheritdoc />
        public int MaxIntegrity
        {
            get => m_MaxIntegrity;
            set => this.RaiseAndSetIfChanged(ref m_MaxIntegrity, value);
        }

        /// <inheritdoc />
        public int CurrentIntegrity
        {
            get => _state.CurrentIntegrity;
            set
            {
                if (_state.CurrentIntegrity != value)
                {
                    _state.CurrentIntegrity = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Public constructor for <see cref="Hull"/> component.
        /// </summary>
        /// <param name="prototype"></param>
        public Hull(IHullPrototype prototype)
        {
            m_MaxIntegrity = prototype.MaxIntegrity;

            _state = new HullState
            {
                Id = Guid.NewGuid(),
                //PrototypeId = prototype.Id,   // TODO: Should Hull have a prototypeId?
                Name = "Hull",                  //       Or name?
                CurrentIntegrity = m_MaxIntegrity,
                PendingDamage = 0,
            };
        }

        /// <inheritdoc />
        public void ApplyDamage()
        {
            CurrentIntegrity = Math.Max(0, MaxIntegrity - _state.PendingDamage);
            _state.PendingDamage = 0;
        }

        /// <inheritdoc />
        public void AssignDamage(int damage)
        {
            _state.PendingDamage += damage;
        }

        #region IHaveState

        public HullState GetState()
        {
            _state.Hash = _state.GetHashCode();
            return _state;
        }

        public void SetState(HullState state)
        {
            _state = state;
        }

        public int GetStateHash()
        {
            return _state.GetHashCode();
        }

        #endregion
    }
}
