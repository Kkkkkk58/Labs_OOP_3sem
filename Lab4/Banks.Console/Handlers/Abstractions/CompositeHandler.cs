namespace Banks.Console.Handlers.Abstractions;

public abstract class CompositeHandler : ICompositeHandler
{
    private readonly List<IHandler> _subHandlers;
    private IHandler? _next;

    protected CompositeHandler(string handledRequest)
    {
        HandledRequest = handledRequest;
        _subHandlers = new List<IHandler>();
    }

    public string HandledRequest { get; }

    public void Handle(params string[] args)
    {
        if (HandledRequest.Equals(args[0], StringComparison.OrdinalIgnoreCase))
        {
            _subHandlers.Single(handler => handler.HandledRequest.Equals(args[1])).Handle(args[1..]);
        }
        else
        {
            if (_next is null)
                throw new NotImplementedException();

            _next.Handle(args[1..]);
        }
    }

    public IHandler SetNext(IHandler handler)
    {
        _next = handler;
        return _next;
    }

    public ICompositeHandler AddSubHandler(IHandler handler)
    {
        _subHandlers.Add(handler);
        return this;
    }
}