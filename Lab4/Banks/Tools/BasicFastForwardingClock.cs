using Banks.EventArgs;
using Banks.Tools.Abstractions;

namespace Banks.Tools;

public class BasicFastForwardingClock : IFastForwardingClock
{
    private EventHandler<DateChangedEventArgs>? _eventHandler;

    public BasicFastForwardingClock(DateTime now)
    {
        Now = now;
    }

    public DateTime Now { get; private set; }

    public void Subscribe(Action<object?, DateChangedEventArgs> update)
    {
        ArgumentNullException.ThrowIfNull(update);
        _eventHandler += (sender, args) => update(sender, args);
    }

    public void Skip(TimeSpan time)
    {
        Now += time;
        _eventHandler?.Invoke(this, new DateChangedEventArgs(Now));
    }

    public void SkipDays(int days)
    {
        if (days < 0)
            throw new ArgumentOutOfRangeException(nameof(days));

        Now = Now.AddDays(days);
        _eventHandler?.Invoke(this, new DateChangedEventArgs(Now));
    }

    public void SkipMonths(int months)
    {
        if (months < 0)
            throw new ArgumentOutOfRangeException(nameof(months));

        Now = Now.AddMonths(months);
        _eventHandler?.Invoke(this, new DateChangedEventArgs(Now));
    }

    public void SkipYears(int years)
    {
        if (years < 0)
            throw new ArgumentOutOfRangeException(nameof(years));

        Now = Now.AddYears(years);
        _eventHandler?.Invoke(this, new DateChangedEventArgs(Now));
    }
}