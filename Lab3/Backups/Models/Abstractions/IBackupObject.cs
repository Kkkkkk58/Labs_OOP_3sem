using Backups.Models.Repository.Abstractions;

namespace Backups.Models.Abstractions;

public interface IBackupObject
{
    IRepositoryAccessKey AccessKey { get; }
    IRepository SourceRepository { get; }
    IRepositoryObject GetRepositoryObject();
}