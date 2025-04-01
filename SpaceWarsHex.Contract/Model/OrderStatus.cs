using System;

namespace SpaceWars.Model
{
    /// <summary>
    /// Status codes for orders. values Based on HTTP response codes.
    /// </summary>
    /// <remarks>
    /// Another way to do this is to throw and catch exceptions. For example: thowing a <see cref="ArgumentOutOfRangeException"/> instead of returning <see cref="NotValid"/>.
    /// However, I am not sure on the overhead of exception handling in godot so this is likely to be faster. Also it forces the calling funciton to deal with invalid inputs instead
    /// of having the chance an un-handled exception crashes things.
    /// </remarks>
    public enum OrderStatus
    {
        /// <summary>The order was accepted and applied.</summary>
        Ok = 200,
        /// <summary>The order was accepted and applied but will not change the state of the ship. Eg. telling a ship already at Attack Power to go to Attack Power.</summary>
        NotModified = 204,
        /// <summary>The order is of a type not supported by the entity.</summary>
        NotAllowed = 405,
        /// <summary>The order is of a supported type, but the input parameters are outside acceptable ranges for the current state of the entity.</summary>
        NotValid = 406,
    }
}