namespace Backups.Models.Abstractions;

public interface IBackupObject
{
    Guid Id { get; }
    IRepositoryAccessKey AccessKey { get; }
    IRepository SourceRepository { get; }
    IReadOnlyList<IRepositoryObject> GetContents();
}