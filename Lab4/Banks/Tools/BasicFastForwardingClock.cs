using Banks.BankAccountWrappers;
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
        _eventHandler += (sender, args) => update(sender, args);
    }

    public void Skip(TimeSpan time)
    {
        Now += time;
        _eventHandler?.Invoke(this, new DateChangedEventArgs(Now));
    }

    public void SkipDays(int days)
    {
        Now = Now.AddDays(days);
        _eventHandler?.Invoke(this, new DateChangedEventArgs(Now));
    }

    public void SkipMonths(int months)
    {
        Now = Now.AddMonths(months);
        _eventHandler?.Invoke(this, new DateChangedEventArgs(Now));
    }

    public void SkipYears(int years)
    {
        Now = Now.AddYears(years);
        _eventHandler?.Invoke(this, new DateChangedEventArgs(Now));
    }
}