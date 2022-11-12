using Backups.Models.Abstractions;

namespace Backups.Exceptions;

public class BackupTaskException : BackupsException
{
    private BackupTaskException(string message)
        : base(message)
    {
    }

    public static BackupTaskException InvalidRestorePointCreationDate(DateTime restorePointDate)
    {
        return new BackupTaskException($"Creation date {restorePointDate} is invalid");
    }

    public static BackupTaskException ObjectIsAlreadyTracked(IBackupObject backupObject)
    {
        return new BackupTaskException($"Backup object {backupObject} is already being tracked");
    }

    public static BackupTaskException ObjectNotFound(IBackupObject backupObject)
    {
        return new BackupTaskException($"Backup object {backupObject} was not found");
    }
}