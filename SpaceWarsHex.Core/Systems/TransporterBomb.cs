using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Interfaces.Systems;
using SpaceWarsHex.Model;

namespace SpaceWarsHex.Systems
{
    /// <summary>
    /// Concrete class for a transporter bomb.
    /// </summary>
    public class TransporterBomb : WeaponBase, IOrdinance, IBombLauncher
    {
        #region IOrdinance
        /// <inheritdoc />
        public int Strength { get; private set; }
        /// <inheritdoc />
        public int MaxUses { get; private set; }
        /// <inheritdoc />
        public int UsesRemaining { get; set; }

        #endregion // IOrdinance

        #region IBombLauncher
        /// <inheritdoc />
        public int MaxRange { get; private set; }
        /// <inheritdoc />
        public int DetonationDelay { get; private set; }
        /// <inheritdoc />
        public TurnPhase DetonationPhase { get; private set; }
        /// <inheritdoc />
        public int RevealDelay { get; private set; }
        /// <inheritdoc />
        public TurnPhase RevealPhase { get; private set; }

        #endregion // IBombLauncher

        #region Order State

        /// <inheritdoc />
        public HexVector2? TargetHex { get; set; }

        #endregion // Order State

        /// <summary>
        /// Public constructor.
        /// </summary>
        public TransporterBomb(ITransporterBombPrototype prototype)
            : base(prototype)
        {
            Strength = prototype.Strength;
            MaxUses = prototype.MaxUses;
            UsesRemaining = MaxUses;
            MaxRange = prototype.MaxRange;
            DetonationDelay = prototype.DetonationDelay;
            DetonationPhase = prototype.DetonationPhase;
            RevealDelay = prototype.RevealDelay;
            RevealPhase = prototype.RevealPhase;

            TargetHex = null;
        }

        #region ISystem
        /// <inheritdoc />
        public override void ApplyDamage(int currentHull, int maxHull)
        {
            var multiplier = _damageThresholds.GetThresholdMultiplier(currentHull, maxHull);
            if (multiplier < 1)
            {
                UsesRemaining = 0;
            }
        }
        /// <inheritdoc />
        public override void HandleEndOfTurn(int turnNumber)
        {
            if (TargetHex.HasValue)
            {
                TargetHex = default;
            }
        }

        #endregion // ISystem
    }
}
