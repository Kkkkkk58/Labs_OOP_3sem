using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Entities.Abstractions;
using Banks.Models;

namespace Banks.Console.Handlers.BankHandlers;

public class BankChangeSuspiciousOperationsLimitHandler : Handler
{
    private readonly AppContext _context;

    public BankChangeSuspiciousOperationsLimitHandler(AppContext context)
        : base("susLimit")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        INoTransactionalBank bank = GetBank();
        MoneyAmount suspiciousOperationsLimit = GetSuspiciousOperationsLimit();

        bank.AccountTypeManager.SetSuspiciousOperationsLimit(suspiciousOperationsLimit);
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

    private MoneyAmount GetSuspiciousOperationsLimit()
    {
        _context.Writer.Write("Enter suspicious operations limit: ");
        return _context.Reader.ReadLine().ToMoneyAmount();
    }
}