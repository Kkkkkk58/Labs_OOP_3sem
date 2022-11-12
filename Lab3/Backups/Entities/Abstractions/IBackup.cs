using Backups.Models.Abstractions;

namespace Backups.Entities.Abstractions;

public interface IBackup
{
    Guid Id { get; }
    string Name { get; }
    IReadOnlyCollection<IRestorePoint> RestorePoints { get; }

    IRestorePoint AddRestorePoint(IRestorePoint restorePoint);
    void RemoveRestorePoint(IRestorePoint restorePoint);
}