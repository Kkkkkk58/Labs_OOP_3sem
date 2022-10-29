namespace Backups.Models.Abstractions;

public interface IClock
{
    DateTime Now { get; }
}