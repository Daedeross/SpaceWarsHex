namespace SpaceWarsHex.Interfaces
{
    public interface IWrapper<TEntity>
    {
        TEntity Entity { get; set; }
    }
}
