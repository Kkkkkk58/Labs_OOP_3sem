using Banks.AccountTypes.Abstractions;
using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Entities.Abstractions;

namespace Banks.Console.Handlers.BankHandlers;

public class BankTypeDebitCreateHandler : Handler
{
    private readonly AppContext _context;

    public BankTypeDebitCreateHandler(AppContext context)
        : base("create")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        INoTransactionalBank bank = GetBank();
        IDebitAccountType type = GetType(bank);

        _context.Writer.WriteLine($"Successfully created debit type {type.Id}");
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

    private IDebitAccountType GetType(INoTransactionalBank bank)
    {
        _context.Writer.Write("Enter interest on balance: ");
        decimal interestOnBalance = decimal.Parse(_context.Reader.ReadLine());
        _context.Writer.Write("Enter interest calculation period: ");
        var period = TimeSpan.Parse(_context.Reader.ReadLine());

        return bank.AccountTypeManager.CreateDebitAccountType(interestOnBalance, period);
    }
}