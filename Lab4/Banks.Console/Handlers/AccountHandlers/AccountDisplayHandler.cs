using Banks.BankAccounts.Abstractions;
using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Console.ViewModels;

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
        IUnchangeableBankAccount account = GetAccount();

        _context.Writer.WriteLine(new AccountViewModel(account));
    }

    private IUnchangeableBankAccount GetAccount()
    {
        _context.Writer.WriteLine("Enter account id: ");
        var accountId = _context.Reader.ReadLine().ToGuid();

        return _context
            .CentralBank
            .Banks
            .Single(bank => bank.FindAccount(accountId) is not null)
            .GetAccount(accountId);
    }
}