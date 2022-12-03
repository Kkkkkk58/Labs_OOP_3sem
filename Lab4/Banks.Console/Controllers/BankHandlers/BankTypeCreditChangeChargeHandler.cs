using Banks.Console.Chains;
using Banks.Console.Extensions;
using Banks.Entities.Abstractions;

namespace Banks.Console.Controllers.BankHandlers;

public class BankTypeCreditChangeChargeHandler : Handler
{
    private readonly AppContext _context;

    public BankTypeCreditChangeChargeHandler(AppContext context)
        : base("charge")
    {
        _context = context;
    }

    public override void Handle(params string[] args)
    {
        var bankId = args[1].ToGuid();
        var typeId = args[2].ToGuid();
        var charge = args[3].ToMoneyAmount();

        INoTransactionalBank bank = _context.CentralBank.Banks.Single(b => b.Id.Equals(bankId));
        bank.AccountTypeManager.ChangeCreditCharge(typeId, charge);
        base.Handle(args);
    }
}