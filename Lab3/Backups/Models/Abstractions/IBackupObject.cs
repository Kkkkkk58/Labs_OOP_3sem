namespace Backups.Models.Abstractions;

public interface IBackupObject
{
    IRepositoryAccessKey AccessKey { get; }
    IRepository SourceRepository { get; }
    IRepositoryObject GetRepositoryObject();
}