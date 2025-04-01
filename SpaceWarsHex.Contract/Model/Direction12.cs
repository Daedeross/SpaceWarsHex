namespace SpaceWars.Model
{
    /// <summary>
    /// For specifying direction or orientation in 12 discrete steps arround the hex (All faces AND corners)
    /// </summary>
    /// <remarks>
    /// Pattern is 'D' followed by degrees clockwise.
    /// </remarks>
    public enum Direction12
    {
        /// <summary>
        /// North Face
        /// </summary>
        D0 = 0,

        /// <summary>
        /// North East Vertex
        /// </summary>
        D30 = 1,

        /// <summary>
        /// North East Face
        /// </summary>
        D60 = 2,

        /// <summary>
        /// East Vertex
        /// </summary>
        D90 = 3,

        /// <summary>
        /// South East Face
        /// </summary>
        D120 = 4,

        /// <summary>
        /// South East Vertex
        /// </summary>
        D150 = 5,

        /// <summary>
        /// South Face
        /// </summary>
        D180 = 6,

        /// <summary>
        /// South West Vertex
        /// </summary>
        D210 = 7,

        /// <summary>
        /// South West Face
        /// </summary>
        D240 = 8,

        /// <summary>
        /// West Vertex
        /// </summary>
        D270 = 9,

        /// <summary>
        /// North West Face
        /// </summary>
        D300 = 10,

        /// <summary>
        /// North West Vertex
        /// </summary>
        D330 = 11,
    }
}