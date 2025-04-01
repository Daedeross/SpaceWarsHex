using System;

namespace SpaceWars.Interfaces
{
    /// <summary>
    /// Public interface for any object with a unique identifier
    /// </summary>
    public interface IHaveId
    {
        /// <summary>
        /// The unique id.
        /// </summary>
        public Guid Id { get; set; }
    }
}
