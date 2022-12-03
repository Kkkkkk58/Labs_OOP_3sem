using Banks.BankAccounts.Abstractions;
using Banks.Console.Chains;
using Banks.Console.Extensions;

namespace Banks.Console.Controllers.AccountHandlers;

public class AccountDisplayHandler : Handler
{
    private readonly AppContext _context;
    public AccountDisplayHandler(AppContext context)
        : base("display")
    {
        _context = context;
    }

    public override void Handle(params string[] args)
    {
        var accountId = args[1].ToGuid();
        IUnchangeableBankAccount account = _context.CentralBank.Banks
            .Single(bank => bank.FindAccount(accountId) is not null).GetAccount(accountId);

        System.Console.WriteLine(account);
        base.Handle(args);
    }
}