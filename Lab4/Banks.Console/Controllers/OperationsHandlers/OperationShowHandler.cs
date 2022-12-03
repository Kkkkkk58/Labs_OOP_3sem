using Banks.Console.Chains;
using Banks.Console.Extensions;
using Banks.Models.Abstractions;

namespace Banks.Console.Controllers.OperationsHandlers;

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
        System.Console.WriteLine(operationInformation);
        base.Handle(args);
    }
}