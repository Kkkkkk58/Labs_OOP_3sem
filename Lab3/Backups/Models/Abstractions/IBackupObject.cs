namespace Backups.Models.Abstractions;

public interface IBackupObject
{
    RepositoryAccessKey AccessKey { get; }
    IRepository SourceRepository { get; }
    IReadOnlyList<BackupObjectContent> GetContents();
}