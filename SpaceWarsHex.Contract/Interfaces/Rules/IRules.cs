namespace SpaceWarsHex.Interfaces.Rules
{
    public interface IRules
    {
        public IPhasorTable PhasorTable { get; }
        public IConcussionTable ConcussionTable { get; }
    }
}
