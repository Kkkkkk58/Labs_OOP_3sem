using Banks.Console.Handlers.Abstractions;
using Banks.Console.ViewModels;
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
        foreach (IOperationInformation operationInformation in _context.CentralBank.Operations)
        {
            _context.Writer.WriteLine(new OperationInformationViewModel(operationInformation));
        }
    }
}