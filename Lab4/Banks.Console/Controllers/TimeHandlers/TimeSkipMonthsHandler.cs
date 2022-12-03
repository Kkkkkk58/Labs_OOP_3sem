using Banks.Console.Chains;
using Banks.Tools.Abstractions;

namespace Banks.Console.Controllers.TimeHandlers;

public class TimeSkipMonthsHandler : Handler
{
    private readonly IFastForwardingClock _clock;

    public TimeSkipMonthsHandler(IFastForwardingClock clock)
        : base("months")
    {
        _clock = clock;
    }

    public override void Handle(params string[] args)
    {
        int months = int.Parse(args[1]);
        _clock.SkipMonths(months);

        System.Console.WriteLine(_clock.Now);
        base.Handle(args);
    }
}