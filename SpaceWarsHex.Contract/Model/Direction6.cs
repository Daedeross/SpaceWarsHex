namespace SpaceWars.Model
{
    /// <summary>
    /// For specifying direction or orientation in 6 discrete steps arround the hex (All faces or corners)
    /// </summary>
    /// <remarks>
    ///     N    
    ///     _    
    /// NW / \ NE
    /// SW \_/ SE
    ///     S    
    /// </remarks>
    public enum Direction6
    {
        /// <summary>
        /// North
        /// </summary>
        N = 0,

        /// <summary>
        /// North East
        /// </summary>
        NE = 1,

        /// <summary>
        /// South East
        /// </summary>
        SE = 2,

        /// <summary>
        /// South
        /// </summary>
        S = 3,

        /// <summary>
        /// South West
        /// </summary>
        SW = 4,

        /// <summary>
        /// North West
        /// </summary>
        NW = 5,
    }
}