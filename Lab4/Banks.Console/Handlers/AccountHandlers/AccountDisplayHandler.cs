using Banks.BankAccounts.Abstractions;
using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers.AccountHandlers;

public class AccountDisplayHandler : Handler
{
    private readonly AppContext _context;
    public AccountDisplayHandler(AppContext context)
        : base("display")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        var accountId = args[1].ToGuid();
        IUnchangeableBankAccount account = _context.CentralBank.Banks
            .Single(bank => bank.FindAccount(accountId) is not null).GetAccount(accountId);

        _context.Writer.WriteLine(account);
    }
}