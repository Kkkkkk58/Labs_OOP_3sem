using Banks.Console.Chains;

namespace Banks.Console.Controllers.BankHandlers;

public class BankTypeDepositHandler : CompositeHandler
{
    public BankTypeDepositHandler()
        : base("deposit")
    {
    }
}