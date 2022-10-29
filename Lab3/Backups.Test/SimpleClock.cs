using Backups.Models.Abstractions;

namespace Backups.Test;

public class SimpleClock : IClock
{
    public DateTime Now => DateTime.Now;
}