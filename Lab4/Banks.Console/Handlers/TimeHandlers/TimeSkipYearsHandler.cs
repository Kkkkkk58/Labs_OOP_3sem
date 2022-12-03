using Banks.Console.Handlers.Abstractions;
using Banks.Tools.Abstractions;

namespace Banks.Console.Handlers.TimeHandlers;

public class TimeSkipYearsHandler : Handler
{
    private readonly IFastForwardingClock _clock;
    private readonly AppContext _context;

    public TimeSkipYearsHandler(IFastForwardingClock clock, AppContext context)
        : base("years")
    {
        _clock = clock;
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        int days = int.Parse(args[1]);
        _clock.SkipYears(days);

        _context.Writer.WriteLine(_clock.Now);
    }
}