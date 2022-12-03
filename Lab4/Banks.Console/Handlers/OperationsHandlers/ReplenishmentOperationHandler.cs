using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Models.Abstractions;

namespace Banks.Console.Handlers.OperationsHandlers;

public class ReplenishmentOperationHandler : Handler
{
    private readonly AppContext _context;

    public ReplenishmentOperationHandler(AppContext context)
        : base("replenish")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        var accountId = args[1].ToGuid();
        var moneyAmount = args[2].ToMoneyAmount();

        IOperationInformation operationInformation = _context.CentralBank.Replenish(accountId, moneyAmount);
        _context.Writer.WriteLine($"Transaction {operationInformation.Id} was successful");
    }
}