using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers.BankHandlers;

public class BankTypeDebitHandler : CompositeHandler
{
    public BankTypeDebitHandler()
        : base("debit")
    {
    }
}