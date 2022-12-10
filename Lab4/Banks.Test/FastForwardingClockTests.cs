using Banks.Tools;
using Xunit;

namespace Banks.Test;

public class FastForwardingClockTests
{
    [Fact]
    public void SkipTime_CurrentTimeChanged()
    {
        var clock = new BasicFastForwardingClock(DateTime.Now);
        DateTime currentTime = clock.Now;
        var timeDifference = TimeSpan.FromMinutes(42);
        clock.Skip(timeDifference);
        DateTime newTime = clock.Now;
        Assert.Equal(newTime - currentTime, timeDifference);
    }
}