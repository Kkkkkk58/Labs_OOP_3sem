using Backups.Models.Abstractions;

namespace Backups.LocalFileSystem.Test;

public class SimpleClock : IClock
{
    public DateTime Now => DateTime.Now;
}