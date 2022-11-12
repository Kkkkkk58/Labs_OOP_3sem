using Backups.Entities.Abstractions;
using Backups.Models.Abstractions;
using Backups.Services.BackupTaskService.Configuration.Abstractions;

namespace Backups.Services.BackupTaskService.Abstractions;

public interface IBackupTaskBuilder
{
    IBackupTaskBuilder SetConfiguration(IBackupTaskConfiguration configuration);
    IBackupTaskBuilder SetBackup(IBackup backup);
    IBackupTaskBuilder SetBackupName(string backupName);
    IBackupTaskBuilder SetTrackedObjects(IEnumerable<IBackupObject> trackedObjects);

    IBackupTask Build();
}