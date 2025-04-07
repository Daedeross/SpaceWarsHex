using System;

namespace SpaceWarsHex.Model
{
    /// <summary>
    /// Flags that can be combined to signal to the director that the object needs special handiling for something.
    /// </summary>
    [Flags]
    public enum HexObjectFlags
    {
        /// <summary>
        /// Nothing special
        /// </summary>
        None    = 0,
        /// <summary>
        /// The object is to be hidden.
        /// </summary>
        Hide    = 1,
        /// <summary>
        /// The object is to be disabled.
        /// </summary>
        Disable = 2,
        /// <summary>
        /// The object is to be destoryed.
        /// </summary>
        Distroy = 4,
        /// <summary>
        /// The object is to be removed.
        /// </summary>
        Remove  = 8,
    }
}
