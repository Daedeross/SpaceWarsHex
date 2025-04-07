using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Model;
using System;

namespace SpaceWarsHex.Entities
{
    /// <inheritdoc />
    public abstract class MovingHexObject : HexObject, IMovingHexObject
    {
        private HexVector2 m_Velocity;
        /// <inheritdoc />
        public HexVector2 Velocity
        {
            get => m_Velocity;
            set => this.RaiseAndSetIfChanged(ref m_Velocity, value);
        }

        private TurnPhase m_MovementPhase;
        /// <inheritdoc />
        public TurnPhase MovementPhase
        {
            get => m_MovementPhase;
            set => this.RaiseAndSetIfChanged(ref m_MovementPhase, value);
        }
    }
}
