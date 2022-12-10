using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers.BankHandlers;

public class BankChangeHandler : CompositeHandler
{
    public BankChangeHandler()
        : base("change")
    {
    }
}