using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers.OperationsHandlers;

public class TransferOperationHandler : Handler
{
    private readonly AppContext _context;

    public TransferOperationHandler(AppContext context)
        : base("transfer")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        var fromAccountId = args[1].ToGuid();
        var toAccountId = args[2].ToGuid();
        var moneyAmount = args[3].ToMoneyAmount();
        _context.CentralBank.Transfer(fromAccountId, toAccountId, moneyAmount);
    }
}