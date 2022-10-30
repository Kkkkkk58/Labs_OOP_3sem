using Backups.Entities.Abstractions;
using Backups.Models.Abstractions;

namespace Backups.Exceptions;

public class BackupException : BackupsException
{
    private BackupException(string message)
        : base(message)
    {
    }

    public static BackupException RestorePointAlreadyExists(IRestorePoint restorePoint, IBackup backup)
    {
        throw new BackupException($"{restorePoint} already exists in backup {backup.Id}");
    }

    public static BackupException RestorePointNotFound(IRestorePoint restorePoint, IBackup backup)
    {
        throw new BackupException($"{restorePoint} wasn't found in backup {backup.Id}");
    }
}