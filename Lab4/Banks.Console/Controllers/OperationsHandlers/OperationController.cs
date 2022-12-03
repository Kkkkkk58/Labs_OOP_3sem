using Banks.Console.Chains;

namespace Banks.Console.Controllers.OperationsHandlers;

public class OperationController : CompositeHandler
{
    public OperationController()
        : base("operation")
    {
    }
}