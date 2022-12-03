using Banks.Console.Chains;

namespace Banks.Console.Controllers.CustomerHandlers;

public class CustomerInformationHandler : CompositeHandler
{
    public CustomerInformationHandler()
        : base("info")
    {
    }
}