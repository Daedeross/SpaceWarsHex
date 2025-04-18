﻿using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Interfaces.Systems;
using SpaceWarsHex.Model;
using SpaceWarsHex.States.Systems;

namespace SpaceWarsHex.Systems
{
    /// <inheritdoc />
    public class Drive : SystemBase, IDrive, IHaveState<SystemStateBase>
    {
        private SystemStateBase _state;
        /// <summary>
        /// TODO: replace with localized strings later.
        /// </summary>
        public override string Name => "Drive";

        /// <inheritdoc />
        public int MaxWarp { get; protected set; }

        /// <inheritdoc />
        public int AccelerationClass { get; protected set; }

        /// <inheritdoc />
        public HexVector2 Velocity { get; set; }

        /// <inheritdoc />
        public HexVector2 Acceleration { get; set; }

        /// <inheritdoc />
        public Drive(IDrivePrototype prototype)
            : base(prototype)
        {
            MaxWarp = prototype.MaxWarp;
            AccelerationClass = prototype.AccelerationClass;

            Velocity = HexVector2.Zero;

            _state = new()
            {
                Id = Guid.NewGuid(),
                PrototypeId = prototype.Id,
                Name = prototype.Name,
            };
        }

        /// <inheritdoc />
        public override void ApplyDamage(int currentHull, int maxHull)
        {
            // NOOP
        }

        /// <inheritdoc />
        public override void HandleEndOfTurn(int turnNumber)
        {
        }

        #region IHaveState

        public SystemStateBase GetState()
        {
            _state.Hash = _state.GetHashCode();
            return _state;
        }

        public void SetState(SystemStateBase state)
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
