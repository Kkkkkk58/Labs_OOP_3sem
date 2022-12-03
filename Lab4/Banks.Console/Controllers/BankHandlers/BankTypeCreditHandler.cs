using Banks.Console.Chains;

namespace Banks.Console.Controllers.BankHandlers;

public class BankTypeCreditHandler : CompositeHandler
{
    public BankTypeCreditHandler()
        : base("credit")
    {
    }
}