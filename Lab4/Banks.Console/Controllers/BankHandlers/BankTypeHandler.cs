using Banks.Console.Chains;

namespace Banks.Console.Controllers.BankHandlers;

public class BankTypeHandler : CompositeHandler
{
    public BankTypeHandler()
        : base("type")
    {
    }
}