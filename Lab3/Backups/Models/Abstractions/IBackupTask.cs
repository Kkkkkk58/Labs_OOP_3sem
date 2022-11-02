using Backups.Entities.Abstractions;

namespace Backups.Models.Abstractions;

public interface IBackupTask
{
    IBackup Backup { get; }
    IRestorePoint CreateRestorePoint();
    IBackupObject TrackBackupObject(IBackupObject backupObject);
    void UntrackBackupObject(IBackupObject backupObject);
}