using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Models.Abstractions;

namespace Banks.Console.Handlers.OperationsHandlers;

public class OperationShowHandler : Handler
{
    private readonly AppContext _context;
    public OperationShowHandler(AppContext context)
        : base("show")
    {
        _context = context;
    }

    public override void Handle(params string[] args)
    {
        var operationId = args[1].ToGuid();
        IOperationInformation operationInformation =
            _context.CentralBank.Operations.Single(operation => operation.Id.Equals(operationId));
        _context.Writer.WriteLine(operationInformation);
        base.Handle(args);
    }
}