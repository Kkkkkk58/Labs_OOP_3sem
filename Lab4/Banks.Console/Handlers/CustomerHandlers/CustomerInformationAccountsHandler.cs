using Banks.BankAccounts.Abstractions;
using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Console.ViewModels;

namespace Banks.Console.Handlers.CustomerHandlers;

public class CustomerInformationAccountsHandler : Handler
{
    private readonly AppContext _context;
    public CustomerInformationAccountsHandler(AppContext context)
        : base("accounts")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        var customerId = args[1].ToGuid();

        foreach (IUnchangeableBankAccount account in _context.CentralBank.Banks.SelectMany(bank => bank.GetAccounts(customerId)))
        {
            _context.Writer.WriteLine(new AccountViewModel(account));
        }
    }
}