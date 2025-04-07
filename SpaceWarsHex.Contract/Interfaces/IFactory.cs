using SpaceWarsHex.Interfaces.Prototypes;

namespace SpaceWarsHex.Interfaces
{
    /// <summary>
    /// Root interface for factories that take in prototypes and spit out an in-game object
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public interface IFactory<TInput, TOutput>
        where TInput : IPrototype
    {
        /// <summary>
        /// Creates a new instance of <typeparamref name="TOutput"/> based on a prototype.
        /// </summary>
        /// <param name="input">The prototype</param>
        /// <returns>The new <typeparamref name="TOutput"/> based on the prototype.</returns>
        TOutput Create(TInput input);
    }
}
