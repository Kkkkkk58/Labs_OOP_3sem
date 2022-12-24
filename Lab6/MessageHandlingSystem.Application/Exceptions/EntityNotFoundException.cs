namespace MessageHandlingSystem.Application.Exceptions;

public class EntityNotFoundException<T> : ApplicationException
{
    private EntityNotFoundException(string? message)
        : base(message) { }

    public static EntityNotFoundException<T> Create(Guid id)
    {
        return new EntityNotFoundException<T>($"{typeof(T).Name} with id {id} was not found.");
    }
}