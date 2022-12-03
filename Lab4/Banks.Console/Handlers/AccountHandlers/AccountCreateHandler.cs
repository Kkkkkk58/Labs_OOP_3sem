using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers.AccountHandlers;

public class AccountCreateHandler : CompositeHandler
{
    public AccountCreateHandler()
        : base("create")
    {
    }
}