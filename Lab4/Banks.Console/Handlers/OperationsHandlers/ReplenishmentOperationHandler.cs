using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Models;
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
        Guid accountId = GetAccountId();
        MoneyAmount moneyAmount = GetMoneyAmount();

        IOperationInformation operationInformation = _context.CentralBank.Replenish(accountId, moneyAmount);
        _context.Writer.WriteLine($"Transaction {operationInformation.Id} was successful");
    }

    private Guid GetAccountId()
    {
        _context.Writer.Write("Enter account id: ");
        return _context.Reader.ReadLine().ToGuid();
    }

    private MoneyAmount GetMoneyAmount()
    {
        _context.Writer.Write("Enter money amount: ");
        return _context.Reader.ReadLine().ToMoneyAmount();
    }
}