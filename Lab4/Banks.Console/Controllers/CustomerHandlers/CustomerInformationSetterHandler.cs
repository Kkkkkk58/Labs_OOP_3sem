using Banks.Console.Chains;

namespace Banks.Console.Controllers.CustomerHandlers;

public class CustomerInformationSetterHandler : CompositeHandler
{
    public CustomerInformationSetterHandler()
        : base("set")
    {
    }
}