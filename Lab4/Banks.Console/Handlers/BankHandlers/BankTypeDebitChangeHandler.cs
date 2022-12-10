using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers.BankHandlers;

public class BankTypeDebitChangeHandler : CompositeHandler
{
    public BankTypeDebitChangeHandler()
        : base("change")
    {
    }
}