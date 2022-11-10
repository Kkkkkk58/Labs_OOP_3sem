using Backups.Entities.Abstractions;
using Backups.Models.Abstractions;

namespace Backups.Services.BackupTaskService.Abstractions;

public interface IBackupTask
{
    IBackup Backup { get; }
    IRestorePoint CreateRestorePoint();
    IBackupObject TrackBackupObject(IBackupObject backupObject);
    void UntrackBackupObject(IBackupObject backupObject);
}