namespace SpaceWarsHex.Interfaces
{
    public interface IHaveState<TState>
        where TState : class, new()
    {
        TState GetState();
        void SetState(TState state);
        int GetStateHash();
    }
}
