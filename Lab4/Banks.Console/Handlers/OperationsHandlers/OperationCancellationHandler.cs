using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Models;

namespace Banks.Console.Handlers.OperationsHandlers;

public class OperationCancellationHandler : Handler
{
    private readonly AppContext _context;

    public OperationCancellationHandler(AppContext context)
        : base("cancel")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        var operationId = args[1].ToGuid();
        _context.CentralBank.CancelTransaction(operationId);
        _context.Writer.WriteLine($"Transaction {operationId} cancellation was successful");
    }
}