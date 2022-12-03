namespace Banks.Console.Chains;

public abstract class Handler : IHandler
{
    private IHandler? _next;

    protected Handler(string handledRequest)
    {
        HandledRequest = handledRequest;
    }

    public string HandledRequest { get; }
    public virtual void Handle(params string[] args)
    {
        _next?.Handle(args[1..]);
    }

    public IHandler SetNext(IHandler handler)
    {
        _next = handler;
        return _next;
    }
}