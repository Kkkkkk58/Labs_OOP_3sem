using Banks.Console.Handlers.Abstractions;
using Banks.Models.Abstractions;

namespace Banks.Console.Handlers.OperationsHandlers;

public class OverallOperationHistoryHandler : Handler
{
    private readonly AppContext _context;

    public OverallOperationHistoryHandler(AppContext context)
        : base("overall")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        // TODO Top(n)
        // TODO OperationInfo toString or make DTO
        foreach (IOperationInformation operationInformation in _context.CentralBank.Operations)
        {
            _context.Writer.WriteLine(operationInformation);
        }
    }
}