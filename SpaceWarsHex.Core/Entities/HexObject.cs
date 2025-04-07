using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Model;
using System;

namespace SpaceWarsHex.Entities
{
    /// <inheritdoc />
    public abstract class HexObject : NotificationObject, IHexObject
    {
        private string m_Name = string.Empty;
        /// <inheritdoc />
        public string Name
        {
            get => m_Name;
            set => this.RaiseAndSetIfChanged(ref m_Name, value);
        }

        private IPlayer? m_Player;
        /// <inheritdoc />
        public IPlayer? Player
        {
            get => m_Player;
            set => this.RaiseAndSetIfChanged(ref m_Player, value);
        }

        protected HexVector2 m_Position = new HexVector2();
        /// <inheritdoc />
        public HexVector2 Position
        {
            get => m_Position;
            set
            {
                if (m_Position != value)
                {
                    var oldPosition = m_Position;
                    m_Position = value;
                    RaisePositionChanged(oldPosition, value);
                    RaisePropertyChanged(nameof(Position));
                }
            }
        }

        private HexObjectFlags m_Flags;
        /// <inheritdoc />
        public HexObjectFlags Flags
        {
            get => m_Flags;
            set => this.RaiseAndSetIfChanged(ref m_Flags, value);
        }

        private Guid m_Id;
        /// <inheritdoc />
        public Guid Id
        {
            get => m_Id;
            set => this.RaiseAndSetIfChanged(ref m_Id, value);
        }

        /// <inheritdoc />
        public abstract bool HandleEndOfPhase(TurnPhase turnPhase);

        /// <inheritdoc />
        public abstract bool HandleEndOfTurn(int turnNumber);


        protected void RaisePositionChanged(HexVector2 oldPosition, HexVector2 newPosition)
        {
            PositionChanged?.Invoke(this, new PositionChangedEventArgs(oldPosition, newPosition));
        }

        public event PositionChangedEventHandler? PositionChanged;
    }
}
