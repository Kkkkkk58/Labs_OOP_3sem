using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers.OperationsHandlers;

public class ReplenishmentOperationHandler : Handler
{
    private readonly AppContext _context;

    public ReplenishmentOperationHandler(AppContext context)
        : base("replenish")
    {
        _context = context;
    }

    public override void Handle(params string[] args)
    {
        var accountId = args[1].ToGuid();
        var moneyAmount = args[2].ToMoneyAmount();

        // TODO return operation information after launch and show id
        _context.CentralBank.Replenish(accountId, moneyAmount);

        base.Handle(args);
    }
}