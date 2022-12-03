using Banks.Console.Chains;

namespace Banks.Console.Controllers.BankHandlers;

public class BankChangeHandler : CompositeHandler
{
    public BankChangeHandler()
        : base("change")
    {
    }
}