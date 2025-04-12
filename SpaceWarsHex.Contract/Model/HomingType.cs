namespace SpaceWarsHex.Model
{
    public enum HomingType
    {
        /// <summary>No homing.</summary>
        None     = 0,
        /// <summary>Can manoeuvre every turn.</summary>
        Drone    = 1,
        /// <summary>Can manoeuvre on turn it explodes.</summary>
        Terminal = 2,
    }
}
