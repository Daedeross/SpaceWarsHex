using SpaceWars.Interfaces.Prototypes;
using SpaceWars.Interfaces.Systems;
using SpaceWars.Model;

namespace SpaceWars.Systems
{
    /// <inheritdoc />
    public class Drive : SystemBase, IDrive
    {
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
    }
}
