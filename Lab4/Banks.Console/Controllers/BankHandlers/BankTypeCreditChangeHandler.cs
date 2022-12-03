using Banks.Console.Chains;

namespace Banks.Console.Controllers.BankHandlers;

public class BankTypeCreditChangeHandler : CompositeHandler
{
    public BankTypeCreditChangeHandler()
        : base("change")
    {
    }
}