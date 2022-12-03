using Banks.Console.Chains;

namespace Banks.Console.Controllers.AccountHandlers;

public class AccountController : CompositeHandler
{
    public AccountController()
        : base("account")
    {
    }
}