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
        Guid customerId = GetCustomerId();

        foreach (IUnchangeableBankAccount account in GetCustomerAccounts(customerId))
        {
            _context.Writer.WriteLine(new AccountViewModel(account));
        }
    }

    private IEnumerable<IUnchangeableBankAccount> GetCustomerAccounts(Guid customerId)
    {
        return _context
            .CentralBank
            .Banks
            .SelectMany(bank => bank.GetAccounts(customerId));
    }

    private Guid GetCustomerId()
    {
        _context.Writer.Write("Enter customer id: ");
        return _context.Reader.ReadLine().ToGuid();
    }
}