using Banks.Console.Chains;

namespace Banks.Console.Controllers.BankHandlers;

public class BankController : CompositeHandler
{
    public BankController()
        : base("bank")
    {
    }
}