using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Entities;
using Banks.Entities.Abstractions;

namespace Banks.Console.Handlers.BankHandlers;

public class BankCreateHandler : Handler
{
    private readonly AppContext _context;
    public BankCreateHandler(AppContext context)
        : base("create")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        IBank bank = GetBank();
        _context.CentralBank.RegisterBank(bank);
        _context.Writer.WriteLine($"Successfully created new bank {bank.Id}");
    }

    private IBank GetBank()
    {
        _context.Writer.Write("Enter bank's name: ");
        string name = _context.Reader.ReadLine();
        _context.Writer.Write("Enter suspicious operations limit: ");
        string limit = _context.Reader.ReadLine();

        return new BankBuilder()
            .SetName(name)
            .SetSuspiciousOperationsLimit(limit.ToMoneyAmount())
            .SetAccountFactory(_context.AccountFactory)
            .SetClock(_context.Clock)
            .Build();
    }
}