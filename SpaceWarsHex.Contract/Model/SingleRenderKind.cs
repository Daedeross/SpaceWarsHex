namespace SpaceWarsHex.Model
{
    /// <summary>
    /// Specifies the rendering type for a single renderable object.
    /// </summary>
    /// <remarks>This enumeration defines the various rendering modes that can be applied to an object, such
    /// as sprites, animations, and meshes. The values indicate the specific rendering behavior, including variations
    /// for directional rendering (e.g., 6-way or 12-way).</remarks>
    public enum SingleRenderKind
    {
        NA = 0,
        Sprite = 1,
        Sprite6Way = 2,
        Sprite12Way = 3,
        Animation = 4,
        Animation6Way = 5,
        Animation12Way = 6,
        Mesh = 7,
        SkinnedMesh = 8
    }
}
