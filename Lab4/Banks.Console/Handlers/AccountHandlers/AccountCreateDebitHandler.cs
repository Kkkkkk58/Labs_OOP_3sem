using Banks.AccountTypes.Abstractions;
using Banks.BankAccounts.Abstractions;
using Banks.Entities.Abstractions;
using Banks.Models;

namespace Banks.Console.Handlers.AccountHandlers;

public class AccountCreateDebitHandler : AccountCreateHandlerBase
{
    private readonly AppContext _context;

    public AccountCreateDebitHandler(AppContext context)
        : base("debit", context)
    {
        _context = context;
    }

    protected override void CreateAccount(
        INoTransactionalBank bank,
        IAccountType type,
        ICustomer customer,
        MoneyAmount? balance)
    {
        IUnchangeableBankAccount account = bank.CreateDebitAccount(type, customer, balance);
        _context.Writer.WriteLine($"Successfully created new debit account {account.Id}");
    }
}