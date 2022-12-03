using Banks.Console.Chains;

namespace Banks.Console.Controllers.BankHandlers;

public class BankTypeDepositChangeHandler : CompositeHandler
{
    public BankTypeDepositChangeHandler()
        : base("change")
    {
    }
}