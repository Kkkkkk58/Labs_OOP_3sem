using Banks.AccountTypes.Abstractions;
using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Entities.Abstractions;
using Banks.Models;

namespace Banks.Console.Handlers.AccountHandlers;

public abstract class AccountCreateHandlerBase : Handler
{
    private readonly AppContext _context;

    protected AccountCreateHandlerBase(string handledRequest, AppContext context)
        : base(handledRequest)
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        INoTransactionalBank bank = GetBank();
        IAccountType type = GetType(bank);
        ICustomer customer = GetCustomer(bank);
        MoneyAmount? balance = GetBalance();

        CreateAccount(bank, type, customer, balance);
    }

    protected abstract void CreateAccount(
        INoTransactionalBank bank,
        IAccountType type,
        ICustomer customer,
        MoneyAmount? balance);

    private INoTransactionalBank GetBank()
    {
        _context.Writer.Write("Enter bank id: ");
        var bankId = _context.Reader.ReadLine().ToGuid();

        return _context
            .CentralBank
            .Banks
            .Single(b => b.Id.Equals(bankId));
    }

    private IAccountType GetType(INoTransactionalBank bank)
    {
        _context.Writer.Write("Enter account type id: ");
        var typeId = _context.Reader.ReadLine().ToGuid();
        return bank.AccountTypeManager.GetAccountType(typeId);
    }

    private ICustomer GetCustomer(INoTransactionalBank bank)
    {
        _context.Writer.Write("Enter customer id: ");
        var clientId = _context.Reader.ReadLine().ToGuid();

        return bank
            .Customers
            .Single(c => c.Id.Equals(clientId));
    }

    private MoneyAmount? GetBalance()
    {
        _context.Writer.Write("Enter initial balance [optional]: ");
        string input = _context.Reader.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
            return null;

        return input.ToMoneyAmount();
    }
}