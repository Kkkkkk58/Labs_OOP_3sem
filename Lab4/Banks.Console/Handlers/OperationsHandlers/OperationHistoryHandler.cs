using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers.OperationsHandlers;

public class OperationHistoryHandler : CompositeHandler
{
    public OperationHistoryHandler()
        : base("history")
    {
    }
}