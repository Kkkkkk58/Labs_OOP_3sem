using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;

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
        Guid operationId = GetOperationId();
        _context.CentralBank.CancelTransaction(operationId);
        _context.Writer.WriteLine($"Transaction {operationId} cancellation was successful");
    }

    private Guid GetOperationId()
    {
        _context.Writer.Write("Enter operation id: ");
        return _context.Reader.ReadLine().ToGuid();
    }
}