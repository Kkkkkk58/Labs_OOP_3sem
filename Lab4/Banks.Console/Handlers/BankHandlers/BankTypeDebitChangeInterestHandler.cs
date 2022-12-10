using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Entities.Abstractions;

namespace Banks.Console.Handlers.BankHandlers;

public class BankTypeDebitChangeInterestHandler : Handler
{
    private readonly AppContext _context;

    public BankTypeDebitChangeInterestHandler(AppContext context)
        : base("interest")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        INoTransactionalBank bank = GetBank();
        Guid typeId = GetTypeId();
        decimal interestOnBalance = GetInterestOnBalance();
        bank.AccountTypeManager.ChangeInterestOnBalance(typeId, interestOnBalance);
    }

    private INoTransactionalBank GetBank()
    {
        _context.Writer.Write("Enter bank id: ");
        var bankId = _context.Reader.ReadLine().ToGuid();

        return _context
            .CentralBank
            .Banks
            .Single(b => b.Id.Equals(bankId));
    }

    private Guid GetTypeId()
    {
        _context.Writer.Write("Enter type id: ");
        return _context.Reader.ReadLine().ToGuid();
    }

    private decimal GetInterestOnBalance()
    {
        _context.Writer.Write("Enter interest on balance: ");
        return decimal.Parse(_context.Reader.ReadLine());
    }
}