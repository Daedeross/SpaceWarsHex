using Godot;

namespace SpaceWarsHex.Bridges
{
    public static class Helpers
    {
        public static Vector3 ToVector3(this Vector2 v, float z = 0)
        {
            return new Vector3(v.X, v.Y, z);
        }
    }
}
