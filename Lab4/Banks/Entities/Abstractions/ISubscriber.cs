namespace Banks.Entities.Abstractions;

public interface ISubscriber<in T>
where T : System.EventArgs
{
    Guid Id { get; }
    void Update(object? sender, T eventArgs);
}