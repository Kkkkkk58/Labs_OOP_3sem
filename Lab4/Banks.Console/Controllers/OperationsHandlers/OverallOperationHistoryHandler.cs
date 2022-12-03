using Banks.Console.Chains;
using Banks.Models.Abstractions;

namespace Banks.Console.Controllers.OperationsHandlers;

public class OverallOperationHistoryHandler : Handler
{
    private readonly AppContext _context;

    public OverallOperationHistoryHandler(AppContext context)
        : base("overall")
    {
        _context = context;
    }

    public override void Handle(params string[] args)
    {
        // TODO Top(n)
        // TODO OperationInfo toString or make DTO
        foreach (IOperationInformation operationInformation in _context.CentralBank.Operations)
        {
            System.Console.WriteLine(operationInformation);
        }

        base.Handle(args);
    }
}