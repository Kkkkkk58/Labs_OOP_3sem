using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Entities.Abstractions;
using Banks.Models;

namespace Banks.Console.Handlers.BankHandlers;

public class BankTypeCreditChangeChargeHandler : Handler
{
    private readonly AppContext _context;

    public BankTypeCreditChangeChargeHandler(AppContext context)
        : base("charge")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        INoTransactionalBank bank = GetBank();
        Guid typeId = GetTypeId();
        MoneyAmount charge = GetCharge();

        bank.AccountTypeManager.ChangeCreditCharge(typeId, charge);
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

    private MoneyAmount GetCharge()
    {
        _context.Writer.Write("Enter money amount: ");
        return _context.Reader.ReadLine().ToMoneyAmount();
    }
}