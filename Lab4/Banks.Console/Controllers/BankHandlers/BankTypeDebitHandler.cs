using Banks.Console.Chains;

namespace Banks.Console.Controllers.BankHandlers;

public class BankTypeDebitHandler : CompositeHandler
{
    public BankTypeDebitHandler()
        : base("debit")
    {
    }
}