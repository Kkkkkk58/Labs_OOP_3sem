using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers.TimeHandlers;

public class TimeController : CompositeHandler
{
    public TimeController()
        : base("time")
    {
    }
}