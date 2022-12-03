using Banks.Console.Chains;

namespace Banks.Console.Controllers.TimeHandlers;

public class TimeController : CompositeHandler
{
    public TimeController()
        : base("time")
    {
    }
}