using Banks.Console.Chains;

namespace Banks.Console.Controllers.CustomerHandlers;

public class CustomerController : CompositeHandler
{
    public CustomerController()
        : base("client")
    {
    }
}