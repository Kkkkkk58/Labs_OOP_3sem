using Banks.Console.Chains;
using Banks.Tools.Abstractions;

namespace Banks.Console.Controllers.TimeHandlers;

public class TimeSkipYearsHandler : Handler
{
    private readonly IFastForwardingClock _clock;

    public TimeSkipYearsHandler(IFastForwardingClock clock)
        : base("years")
    {
        _clock = clock;
    }

    public override void Handle(params string[] args)
    {
        int days = int.Parse(args[1]);
        _clock.SkipYears(days);

        System.Console.WriteLine(_clock.Now);
        base.Handle(args);
    }
}