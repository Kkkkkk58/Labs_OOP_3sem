using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Models.Abstractions;

namespace Banks.Console.Handlers.OperationsHandlers;

public class WithdrawalOperationHandler : Handler
{
    private readonly AppContext _context;

    public WithdrawalOperationHandler(AppContext context)
        : base("withdraw")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        var accountId = args[1].ToGuid();
        var moneyAmount = args[2].ToMoneyAmount();
        IOperationInformation operationInformation = _context.CentralBank.Withdraw(accountId, moneyAmount);
        _context.Writer.WriteLine($"Transaction {operationInformation.Id} was successful");
    }
}