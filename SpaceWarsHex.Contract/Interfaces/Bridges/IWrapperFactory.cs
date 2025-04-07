namespace SpaceWarsHex.Interfaces.Bridges
{
    public interface IWrapperFactory<TEntity, TWrapper>
        where TWrapper: IWrapper<TEntity>
    {
        TWrapper CreateWrapper(TEntity entity);
    }
}
