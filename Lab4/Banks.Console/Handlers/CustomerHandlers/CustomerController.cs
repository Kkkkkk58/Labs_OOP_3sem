using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers.CustomerHandlers;

public class CustomerController : CompositeHandler
{
    public CustomerController()
        : base("client")
    {
    }
}