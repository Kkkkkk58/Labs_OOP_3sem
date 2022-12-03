using Banks.Console.Chains;

namespace Banks.Console.Controllers.AccountHandlers;

public class AccountCreateHandler : CompositeHandler
{
    public AccountCreateHandler()
        : base("create")
    {
    }
}