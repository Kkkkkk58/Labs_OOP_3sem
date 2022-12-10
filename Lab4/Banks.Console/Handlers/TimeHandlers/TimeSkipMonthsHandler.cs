using Banks.Console.Handlers.Abstractions;
using Banks.Tools.Abstractions;

namespace Banks.Console.Handlers.TimeHandlers;

public class TimeSkipMonthsHandler : Handler
{
    private readonly IFastForwardingClock _clock;
    private readonly AppContext _context;

    public TimeSkipMonthsHandler(IFastForwardingClock clock, AppContext context)
        : base("months")
    {
        _clock = clock;
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        int months = int.Parse(args[1]);
        _clock.SkipMonths(months);

        _context.Writer.WriteLine($"Current time is {_clock.Now}");
    }
}