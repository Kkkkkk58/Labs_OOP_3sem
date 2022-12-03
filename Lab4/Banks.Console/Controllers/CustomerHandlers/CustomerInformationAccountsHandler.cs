using Banks.BankAccounts.Abstractions;
using Banks.Console.Chains;
using Banks.Console.Extensions;

namespace Banks.Console.Controllers.CustomerHandlers;

public class CustomerInformationAccountsHandler : Handler
{
    private readonly AppContext _context;
    public CustomerInformationAccountsHandler(AppContext context)
        : base("accounts")
    {
        _context = context;
    }

    public override void Handle(params string[] args)
    {
        var customerId = args[1].ToGuid();

        foreach (IUnchangeableBankAccount account in _context.CentralBank.Banks.SelectMany(bank => bank.GetAccounts(customerId)))
        {
            System.Console.WriteLine(account);
        }

        base.Handle(args);
    }
}