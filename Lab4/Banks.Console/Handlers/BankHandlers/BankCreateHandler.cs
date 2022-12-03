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

    public override void Handle(params string[] args)
    {
        IBank bank = GetBank();
        _context.CentralBank.RegisterBank(bank);
        _context.Writer.WriteLine(bank.Id);
        base.Handle(args);
    }

    public IBank GetBank()
    {
        _context.Writer.Write("Enter bank's name: ");
        string name = System.Console.ReadLine() ?? throw new NotImplementedException();
        _context.Writer.Write("Enter suspicious operations limit: ");
        string limit = System.Console.ReadLine() ?? throw new NotImplementedException();

        return new BankBuilder()
            .SetName(name)
            .SetSuspiciousOperationsLimit(limit.ToMoneyAmount())
            .SetAccountFactory(_context.AccountFactory)
            .SetClock(_context.Clock)
            .Build();
    }
}