using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers.CustomerHandlers;

public class CustomerInformationSetterHandler : CompositeHandler
{
    public CustomerInformationSetterHandler()
        : base("set")
    {
    }
}