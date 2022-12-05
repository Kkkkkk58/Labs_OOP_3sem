using Banks.AccountTypes.Abstractions;
using Banks.BankAccounts.Abstractions;
using Banks.Entities.Abstractions;
using Banks.Models;

namespace Banks.Console.Handlers.AccountHandlers;

public class AccountCreateCreditHandler : AccountCreateHandlerBase
{
    private readonly AppContext _context;

    public AccountCreateCreditHandler(AppContext context)
        : base("credit", context)
    {
        _context = context;
    }

    protected override void CreateAccount(
        INoTransactionalBank bank,
        IAccountType type,
        ICustomer customer,
        MoneyAmount? balance)
    {
        IUnchangeableBankAccount account = bank.CreateCreditAccount(type, customer, balance);
        _context.Writer.WriteLine($"Successfully created new credit account {account.Id}");
    }
}