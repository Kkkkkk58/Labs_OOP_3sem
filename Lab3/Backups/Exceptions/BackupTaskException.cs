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
        throw new BackupTaskException($"Creation date {restorePointDate} is invalid");
    }

    public static BackupTaskException ObjectIsAlreadyTracked(IBackupObject backupObject)
    {
        throw new BackupTaskException($"Backup object {backupObject.Id} is already being tracked");
    }

    public static BackupTaskException ObjectNotFound(IBackupObject backupObject)
    {
        throw new BackupTaskException($"Backup object {backupObject.Id} was not found");
    }

    public static BackupTaskException NullVersionString()
    {
        throw new BackupTaskException("Backup task version string must be non-null");
    }
}