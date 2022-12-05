using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Entities.Abstractions;
using Banks.Models;

namespace Banks.Console.Handlers.BankHandlers;

public class BankTypeCreditChangeDebtLimitHandler : Handler
{
    private readonly AppContext _context;

    public BankTypeCreditChangeDebtLimitHandler(AppContext context)
        : base("debtLimit")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        INoTransactionalBank bank = GetBank();
        Guid typeId = GetTypeId();
        MoneyAmount newLimit = GetDebtLimit();

        bank.AccountTypeManager.ChangeDebtLimit(typeId, newLimit);
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

    private MoneyAmount GetDebtLimit()
    {
        _context.Writer.Write("Enter debt limit: ");
        return _context.Reader.ReadLine().ToMoneyAmount();
    }
}