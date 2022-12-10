using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers;

public class MainHandler : CompositeHandler
{
    public MainHandler()
        : base("banks")
    {
    }
}