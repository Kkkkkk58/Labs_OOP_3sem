using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers.BankHandlers;

public class BankTypeHandler : CompositeHandler
{
    public BankTypeHandler()
        : base("type")
    {
    }
}