using SpaceWarsHex.Model;

namespace SpaceWarsHex.Interfaces.Prototypes
{
    public interface ITorpedoPrototye : IHexObjectPrototype
    {
        /// <summary>
        /// The turn phase the torpedo acts.
        /// </summary>
        TurnPhase FirePhase { get; set; }

        /// <summary>
        /// The explosive power of the torepdo
        /// </summary>
        int Strength { get; set; }

        /// <summary>
        /// The homing capabilities, if any
        /// </summary>
        HomingType Homing { get; set; }

        /// <summary>
        /// The Max-Warp lost each time the torpedo manoeuvres.
        /// Only maningful if torpedo is homing.
        /// </summary>
        int HomingLoss { get; set; }

        /// <summary>
        /// The max acceleration of the torpedo, if any.
        /// Only meaninful if torpedo is homing.
        /// </summary>
        int Acceleration { get; set; }
    }
}
