namespace SpaceWars.Interfaces
{
    public interface IWrapper<TEntity>
    {
        TEntity Entity { get; set; }
    }
}
