using Banks.Console.Handlers.Abstractions;
using Banks.Tools.Abstractions;

namespace Banks.Console.Handlers.TimeHandlers;

public class TimeSkipDaysHandler : Handler
{
    private readonly IFastForwardingClock _clock;
    private readonly AppContext _context;

    public TimeSkipDaysHandler(IFastForwardingClock clock, AppContext context)
        : base("days")
    {
        _clock = clock;
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        int days = int.Parse(args[1]);
        _clock.SkipDays(days);

        _context.Writer.WriteLine($"Current time is {_clock.Now}");
    }
}