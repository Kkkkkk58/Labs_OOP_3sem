using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers.CustomerHandlers;

public class CustomerInformationHandler : CompositeHandler
{
    public CustomerInformationHandler()
        : base("info")
    {
    }
}