using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers.BankHandlers;

public class BankTypeDepositHandler : CompositeHandler
{
    public BankTypeDepositHandler()
        : base("deposit")
    {
    }
}