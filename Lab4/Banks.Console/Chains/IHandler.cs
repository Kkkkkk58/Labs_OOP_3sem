namespace Banks.Console.Chains;

public interface IHandler
{
    public string HandledRequest { get; }
    void Handle(params string[] args);
    IHandler SetNext(IHandler handler);
}