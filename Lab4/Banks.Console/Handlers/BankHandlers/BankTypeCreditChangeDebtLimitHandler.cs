using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Entities.Abstractions;

namespace Banks.Console.Handlers.BankHandlers;

public class BankTypeCreditChangeDebtLimitHandler : Handler
{
    private readonly AppContext _context;
    public BankTypeCreditChangeDebtLimitHandler(AppContext context)
        : base("debtLimit")
    {
        _context = context;
    }

    public override void Handle(params string[] args)
    {
        var bankId = args[1].ToGuid();
        var typeId = args[2].ToGuid();
        var newLimit = args[3].ToMoneyAmount();

        INoTransactionalBank bank = _context.CentralBank.Banks.Single(b => b.Id.Equals(bankId));
        bank.AccountTypeManager.ChangeDebtLimit(typeId, newLimit);
        base.Handle(args);
    }
}