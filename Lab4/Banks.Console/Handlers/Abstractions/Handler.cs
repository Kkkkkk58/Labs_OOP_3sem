using Banks.Console.Exceptions;

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
        ArgumentNullException.ThrowIfNull(args);
        if (args.Length < 1)
            throw HandlerException.InvalidRequestParametersLength(args.Length);
        if (!args[0].Equals(HandledRequest))
            throw HandlerException.InvalidRequestType(HandledRequest, args[0]);

        HandleImpl(args);
    }

    protected abstract void HandleImpl(string[] args);
}