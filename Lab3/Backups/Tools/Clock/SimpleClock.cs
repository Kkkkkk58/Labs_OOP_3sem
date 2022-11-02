using Backups.Tools.Clock.Abstractions;

namespace Backups.Tools.Clock;

public class SimpleClock : IClock
{
    public DateTime Now => DateTime.Now;
}