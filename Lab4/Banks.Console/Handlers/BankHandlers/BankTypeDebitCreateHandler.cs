using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Entities.Abstractions;
using Banks.Models.AccountTypes.Abstractions;

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
        var bankId = args[1].ToGuid();
        INoTransactionalBank bank = _context.CentralBank.Banks.Single(b => b.Id.Equals(bankId));
        _context.Writer.Write("Enter interest on balance: ");
        decimal interestOnBalance = decimal.Parse(_context.Reader.ReadLine() ?? throw new NotImplementedException());
        _context.Writer.Write("Enter interest calculation period: ");
        var period = TimeSpan.Parse(_context.Reader.ReadLine() ?? throw new NotImplementedException());

        IDebitAccountType type = bank.AccountTypeManager.CreateDebitAccountType(interestOnBalance, period);

        _context.Writer.WriteLine($"Successfully created debit type {type.Id}");
    }
}