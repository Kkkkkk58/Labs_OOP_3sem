using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers.TimeHandlers;

public class TimeSkipHandler : CompositeHandler
{
    public TimeSkipHandler()
        : base("skip")
    {
    }
}