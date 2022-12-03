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

    public override void Handle(params string[] args)
    {
        var operationId = args[1].ToGuid();
        _context.CentralBank.CancelTransaction(operationId);
        base.Handle(args);
    }
}