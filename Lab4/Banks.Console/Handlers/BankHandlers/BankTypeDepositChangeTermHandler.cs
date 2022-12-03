using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Entities.Abstractions;

namespace Banks.Console.Handlers.BankHandlers;

public class BankTypeDepositChangeTermHandler : Handler
{
    private readonly AppContext _context;
    public BankTypeDepositChangeTermHandler(AppContext context)
        : base("term")
    {
        _context = context;
    }

    public override void Handle(params string[] args)
    {
        var bankId = args[1].ToGuid();
        var typeId = args[2].ToGuid();
        var ts = TimeSpan.Parse(args[3]);

        INoTransactionalBank bank = _context.CentralBank.Banks.Single(b => b.Id.Equals(bankId));
        bank.AccountTypeManager.ChangeDepositTerm(typeId, ts);
        base.Handle(args);
    }
}