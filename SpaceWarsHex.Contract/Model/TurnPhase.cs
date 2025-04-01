using System;

namespace SpaceWars.Model
{
    /// <summary>
    /// The phases that together breakdown of a complete game turn.
    /// </summary>
    /// <remarks>Giveing plenty of space in-between the values of each phase so we can insert more phases as needed and still have
    /// the integer values be in order.</remarks>
    public enum TurnPhase
    {
        /// <summary>Initialization of a new turn.</summary>
        TurnStart = 00,

        /// <summary>Writing Orders</summary>
        Orders = 10,

        /// <summary>Direct-fire and most energy weapons are resolved.</summary>
        Weapons1 = 20,

        /// <summary>Teleportation</summary>
        Displacement = 30,

        /// <summary>Weapons that have some kind of entity or template that is placed on the board are fired or advanced. eg. torpedes, bombs, beams</summary>
        Weapons2 = 40,

        /// <summary>First time Hull damage is applied</summary>
        ApplyDamage1 = 50,

        /// <summary>Smoke and Spray</summary>
        Countermeasures = 60,

        /// <summary>Cloak or decloak</summary>
        Cloaking = 70,

        /// <summary>Ships move</summary>
        Movement = 80,

        /// <summary>Damage from movement is applied.</summary>
        ApplyDamage2 = 90,

        /// <summary>Wrap-up of and finalization of the turn.</summary>
        TurnEnd = 100,
    }

    /// <summary>
    /// Extention Methods for <see cref="TurnPhase"/>
    /// </summary>
    public  static class TurnPhaseExtensions
    {
        /// <summary>
        /// Gets the next turn phase.
        /// </summary>
        /// <param name="phase">The current turn phase</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">The input phase does not exist.</exception>
        public static TurnPhase Next(this TurnPhase phase)
            => phase switch
            {
                TurnPhase.TurnStart => TurnPhase.Orders,
                TurnPhase.Orders => TurnPhase.Weapons1,
                TurnPhase.Weapons1 => TurnPhase.Displacement,
                TurnPhase.Displacement => TurnPhase.Weapons2,
                TurnPhase.Weapons2 => TurnPhase.ApplyDamage1,
                TurnPhase.ApplyDamage1 => TurnPhase.Countermeasures,
                TurnPhase.Countermeasures => TurnPhase.Cloaking,
                TurnPhase.Cloaking => TurnPhase.Movement,
                TurnPhase.Movement => TurnPhase.ApplyDamage2,
                TurnPhase.ApplyDamage2 => TurnPhase.TurnEnd,
                TurnPhase.TurnEnd => TurnPhase.TurnStart,
                _ => throw new NotSupportedException($"Unrecognized TurnPhase {phase}")
            };
    }
}
