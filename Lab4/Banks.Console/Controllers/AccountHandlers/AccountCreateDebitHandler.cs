using Banks.BankAccounts.Abstractions;
using Banks.Console.Chains;
using Banks.Console.Extensions;
using Banks.Entities.Abstractions;
using Banks.Models;
using Banks.Models.AccountTypes.Abstractions;

namespace Banks.Console.Controllers.AccountHandlers;

public class AccountCreateDebitHandler : Handler
{
    private readonly AppContext _context;

    public AccountCreateDebitHandler(AppContext context)
        : base("debit")
    {
        _context = context;
    }

    public override void Handle(params string[] args)
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

        IUnchangeableBankAccount account = bank.CreateDebitAccount(type, customer, balance);
        System.Console.WriteLine(account.Id);
        base.Handle(args);
    }
}