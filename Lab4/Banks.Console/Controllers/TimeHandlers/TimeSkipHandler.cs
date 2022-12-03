using Banks.Console.Chains;

namespace Banks.Console.Controllers.TimeHandlers;

public class TimeSkipHandler : CompositeHandler
{
    public TimeSkipHandler()
        : base("skip")
    {
    }
}