using Banks.Console.Handlers.Abstractions;

namespace Banks.Console.Handlers.OperationsHandlers;

public class OperationController : CompositeHandler
{
    public OperationController()
        : base("operation")
    {
    }
}