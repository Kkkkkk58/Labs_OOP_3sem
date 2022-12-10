using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers.AccountHandlers;

public class AccountController : CompositeHandler
{
    public AccountController()
        : base("account")
    {
    }
}