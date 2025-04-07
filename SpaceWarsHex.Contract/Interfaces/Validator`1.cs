namespace SpaceWarsHex.Interfaces
{
    public interface IValidator<T>
    {
        bool Validate(T value);
    }
}
