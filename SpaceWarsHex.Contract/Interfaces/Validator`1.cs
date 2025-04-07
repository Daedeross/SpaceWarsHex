namespace SpaceWars.Interfaces
{
    public interface IValidator<T>
    {
        bool Validate(T value);
    }
}
