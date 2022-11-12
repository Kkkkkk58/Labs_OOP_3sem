using Backups.Models.Abstractions;

namespace Backups.Services.BackupTaskService.Abstractions;

public interface IBackupTask
{
    IReadOnlyCollection<IRestorePoint> RestorePoints { get; }
    IRestorePoint CreateRestorePoint();
    IBackupObject TrackBackupObject(IBackupObject backupObject);
    void UntrackBackupObject(IBackupObject backupObject);
}