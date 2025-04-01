using SpaceWars.Model;

namespace SpaceWars.Interfaces.Orders
{
    /// <summary>
    /// Order to detonate an existing torpedo.
    /// </summary>
    public interface ITorpedoExplodeOrder : IOrder
    {
        /// <summary>
        /// The hex the torpedo should explode at.
        /// </summary>
        HexVector2 ExplosionPoint { get; }
    }
}
