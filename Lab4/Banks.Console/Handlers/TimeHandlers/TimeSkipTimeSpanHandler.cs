using Banks.Console.Handlers.Abstractions;
using Banks.Tools.Abstractions;

namespace Banks.Console.Handlers.TimeHandlers;

public class TimeSkipTimeSpanHandler : Handler
{
    private readonly IFastForwardingClock _clock;
    private readonly AppContext _context;

    public TimeSkipTimeSpanHandler(IFastForwardingClock clock, AppContext context)
        : base("timespan")
    {
        _clock = clock;
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        var ts = TimeSpan.Parse(args[1]);
        _clock.Skip(ts);

        _context.Writer.WriteLine($"Current time is {_clock.Now}");
    }
}