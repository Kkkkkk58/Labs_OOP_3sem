using Banks.AccountTypes.Abstractions;
using Banks.BankAccounts.Abstractions;
using Banks.Entities.Abstractions;
using Banks.Models;

namespace Banks.Console.Handlers.AccountHandlers;

public class AccountCreateDepositHandler : AccountCreateHandlerBase
{
    private readonly AppContext _context;

    public AccountCreateDepositHandler(AppContext context)
        : base("deposit", context)
    {
        _context = context;
    }

    protected override void CreateAccount(
        INoTransactionalBank bank,
        IAccountType type,
        ICustomer customer,
        MoneyAmount? balance)
    {
        IUnchangeableBankAccount account = bank.CreateDepositAccount(type, customer, balance);
        _context.Writer.WriteLine($"Successfully created new deposit account {account.Id}");
    }
}