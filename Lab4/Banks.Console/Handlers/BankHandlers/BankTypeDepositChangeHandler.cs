using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers.BankHandlers;

public class BankTypeDepositChangeHandler : CompositeHandler
{
    public BankTypeDepositChangeHandler()
        : base("change")
    {
    }
}