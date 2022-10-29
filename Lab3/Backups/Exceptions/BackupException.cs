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
}