using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Entities.Abstractions;

namespace Banks.Console.Handlers.BankHandlers;

public class BankChangeSuspiciousOperationsLimitHandler : Handler
{
    private readonly AppContext _context;

    public BankChangeSuspiciousOperationsLimitHandler(AppContext context)
        : base("susLimit")
    {
        _context = context;
    }

    public override void Handle(params string[] args)
    {
        var bankId = args[1].ToGuid();
        var suspiciousOperationsLimit = args[2].ToMoneyAmount();

        INoTransactionalBank bank = _context.CentralBank.Banks.Single(b => b.Id.Equals(bankId));
        bank.AccountTypeManager.SetSuspiciousOperationsLimit(suspiciousOperationsLimit);
        base.Handle(args);
    }
}