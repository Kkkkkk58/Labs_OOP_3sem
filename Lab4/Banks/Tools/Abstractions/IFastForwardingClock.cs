namespace Banks.Tools.Abstractions;

public interface IFastForwardingClock : IClock
{
    void Skip(TimeSpan time);
    void SkipDays(int days);
    void SkipMonths(int months);
    void SkipYears(int years);
}