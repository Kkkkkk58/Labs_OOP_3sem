using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers.BankHandlers;

public class BankController : CompositeHandler
{
    public BankController()
        : base("bank")
    {
    }
}