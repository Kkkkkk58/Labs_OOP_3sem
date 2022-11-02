namespace Backups.Tools.Clock.Abstractions;

public interface IClock
{
    DateTime Now { get; }
}