using Banks.BankAccounts.Abstractions;
using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Entities.Abstractions;
using Banks.Models;
using Banks.Models.AccountTypes.Abstractions;

namespace Banks.Console.Handlers.AccountHandlers;

public class AccountCreateCreditHandler : Handler
{
    private readonly AppContext _context;

    public AccountCreateCreditHandler(AppContext context)
        : base("credit")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        var bankId = args[1].ToGuid();
        INoTransactionalBank bank = _context.CentralBank.Banks.Single(b => b.Id.Equals(bankId));
        var typeId = args[2].ToGuid();
        IAccountType type = bank.AccountTypeManager.GetAccountType(typeId);
        var clientId = args[3].ToGuid();
        ICustomer customer = _context.CentralBank.Banks.SelectMany(b => b.Customers).Distinct()
            .Single(c => c.Id.Equals(clientId));
        MoneyAmount? balance = null;
        if (args.Length > 4)
        {
            balance = args[4].ToMoneyAmount();
        }

        IUnchangeableBankAccount account = bank.CreateCreditAccount(type, customer, balance);
        _context.Writer.WriteLine($"Successfully created new credit account {account.Id}");
    }
}