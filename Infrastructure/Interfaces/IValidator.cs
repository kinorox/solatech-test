namespace Infrastructure.Interfaces
{
    public interface IValidator<in T>
    {
        bool IsValid(T obj);
    }
}
