namespace SpaceWarsHex.Model
{
    /// <summary>
    /// Represents the various states a reactor can be in.
    /// </summary>
    public enum ReactorState : short
    {
        /// <summary>The ship is at Cruising Power.</summary>
        Cruise = 0,

        /// <summary>The ship is at Attack Power</summary>
        Attack = 1,
    }
}