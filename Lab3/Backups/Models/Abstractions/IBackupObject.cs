using Backups.Models.Repository.Abstractions;
using Backups.Models.RepositoryObjects.Abstractions;

namespace Backups.Models.Abstractions;

public interface IBackupObject
{
    IRepositoryAccessKey AccessKey { get; }
    IRepository SourceRepository { get; }
    IRepositoryObject GetRepositoryObject();
}