using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers.BankHandlers;

public class BankTypeCreditHandler : CompositeHandler
{
    public BankTypeCreditHandler()
        : base("credit")
    {
    }
}