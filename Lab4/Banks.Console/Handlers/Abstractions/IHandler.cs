namespace Banks.Console.Handlers.Abstractions;

public interface IHandler
{
    public string HandledRequest { get; }
    void Handle(params string[] args);
}