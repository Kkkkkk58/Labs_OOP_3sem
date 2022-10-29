using Backups.Entities;
using Backups.Models;

namespace Backups.Exceptions;

public class BackupException : BackupsException
{
    private BackupException(string message)
        : base(message)
    {
    }

    public static BackupException RestorePointAlreadyExists(RestorePoint restorePoint, Backup backup)
    {
        throw new BackupException($"{restorePoint} already exists in backup {backup.Id}");
    }

    public static BackupException RestorePointNotFound(RestorePoint restorePoint, Backup backup)
    {
        throw new BackupException($"{restorePoint} wasn't found in backup {backup.Id}");
    }
}