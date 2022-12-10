using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers.BankHandlers;

public class BankTypeCreditChangeHandler : CompositeHandler
{
    public BankTypeCreditChangeHandler()
        : base("change")
    {
    }
}