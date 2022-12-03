using Banks.Console.Chains;

namespace Banks.Console.Controllers.BankHandlers;

public class BankTypeDebitChangeHandler : CompositeHandler
{
    public BankTypeDebitChangeHandler()
        : base("change")
    {
    }
}