namespace Banks.Console.Handlers.Abstractions;

public abstract class Handler : IHandler
{
    protected Handler(string handledRequest)
    {
        HandledRequest = handledRequest;
    }

    public string HandledRequest { get; }
    public void Handle(params string[] args)
    {
        if (!args[0].Equals(HandledRequest))
            throw new NotImplementedException();

        HandleImpl(args);
    }

    protected abstract void HandleImpl(string[] args);
}