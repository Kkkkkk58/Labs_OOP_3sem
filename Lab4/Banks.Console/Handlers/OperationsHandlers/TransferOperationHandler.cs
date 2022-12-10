using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Models;
using Banks.Models.Abstractions;

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
        Guid fromAccountId = GetSenderAccountId();
        Guid toAccountId = GetReceiverAccountId();
        MoneyAmount moneyAmount = GetMoneyAmount();
        IOperationInformation operationInformation =
            _context.CentralBank.Transfer(fromAccountId, toAccountId, moneyAmount);
        _context.Writer.WriteLine($"Transaction {operationInformation.Id} was successful");
    }

    private Guid GetSenderAccountId()
    {
        _context.Writer.Write("Enter sender account id: ");
        return _context.Reader.ReadLine().ToGuid();
    }

    private Guid GetReceiverAccountId()
    {
        _context.Writer.Write("Enter receiver account id: ");
        return _context.Reader.ReadLine().ToGuid();
    }

    private MoneyAmount GetMoneyAmount()
    {
        _context.Writer.Write("Enter money amount: ");
        return _context.Reader.ReadLine().ToMoneyAmount();
    }
}