using Banks.Console.Chains;
using Banks.Tools.Abstractions;

namespace Banks.Console.Controllers.TimeHandlers;

public class TimeSkipTimeSpanHandler : Handler
{
    private IFastForwardingClock _clock;

    public TimeSkipTimeSpanHandler(IFastForwardingClock clock)
        : base("timespan")
    {
        _clock = clock;
    }

    public override void Handle(params string[] args)
    {
        var ts = TimeSpan.Parse(args[1]);
        _clock.Skip(ts);

        System.Console.WriteLine(_clock.Now);
        base.Handle(args);
    }
}