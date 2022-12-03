using Banks.Console.Chains;

namespace Banks.Console.Controllers.OperationsHandlers;

public class OperationHistoryHandler : CompositeHandler
{
    public OperationHistoryHandler()
        : base("history")
    {
    }
}