using Banks.Console.Chains;
using Banks.Console.Extensions;

namespace Banks.Console.Controllers.OperationsHandlers;

public class WithdrawalOperationHandler : Handler
{
    private readonly AppContext _context;

    public WithdrawalOperationHandler(AppContext context)
        : base("withdraw")
    {
        _context = context;
    }

    public override void Handle(params string[] args)
    {
        var accountId = args[1].ToGuid();
        var moneyAmount = args[2].ToMoneyAmount();
        _context.CentralBank.Withdraw(accountId, moneyAmount);

        // TODO Add success message
        base.Handle(args);
    }
}