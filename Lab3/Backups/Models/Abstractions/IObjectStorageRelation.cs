namespace Backups.Models.Abstractions;

public interface IObjectStorageRelation
{
    IBackupObject BackupObject { get; }
    IStorage Storage { get; }
}