namespace Backups.Models.Abstractions;

public interface IBackupObject
{
    IRepositoryAccessKey AccessKey { get; }
    IRepository SourceRepository { get; }
    IReadOnlyList<RepositoryObject> GetContents();
}