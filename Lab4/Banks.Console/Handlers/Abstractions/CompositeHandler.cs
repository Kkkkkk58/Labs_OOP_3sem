using Banks.Console.Exceptions;

namespace Banks.Console.Handlers.Abstractions;

public abstract class CompositeHandler : ICompositeHandler
{
    private readonly List<IHandler> _subHandlers;

    protected CompositeHandler(string handledRequest)
    {
        HandledRequest = handledRequest;
        _subHandlers = new List<IHandler>();
    }

    public string HandledRequest { get; }

    public void Handle(params string[] args)
    {
        ArgumentNullException.ThrowIfNull(args);
        if (args.Length < 2)
            throw HandlerException.InvalidRequestParametersLength(args.Length);

        if (!HandledRequest.Equals(args[0], StringComparison.OrdinalIgnoreCase))
            throw HandlerException.InvalidRequestType(HandledRequest, args[0]);

        _subHandlers
            .Single(handler => handler.HandledRequest.Equals(args[1]))
            .Handle(args[1..]);
    }

    public ICompositeHandler AddSubHandler(IHandler handler)
    {
        _subHandlers.Add(handler);
        return this;
    }
}