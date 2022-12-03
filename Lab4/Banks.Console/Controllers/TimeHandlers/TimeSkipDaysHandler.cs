using Banks.Console.Chains;
using Banks.Tools.Abstractions;

namespace Banks.Console.Controllers.TimeHandlers;

public class TimeSkipDaysHandler : Handler
{
    private readonly IFastForwardingClock _clock;

    public TimeSkipDaysHandler(IFastForwardingClock clock)
        : base("days")
    {
        _clock = clock;
    }

    public override void Handle(params string[] args)
    {
        int days = int.Parse(args[1]);
        _clock.SkipDays(days);

        System.Console.WriteLine(_clock.Now);
        base.Handle(args);
    }
}