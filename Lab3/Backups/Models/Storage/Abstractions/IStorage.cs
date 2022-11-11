using Backups.Models.Abstractions;
using Backups.Models.Repository.Abstractions;
using Backups.Models.RepositoryObjects.Abstractions;

namespace Backups.Models.Storage.Abstractions;

public interface IStorage
{
    IRepositoryAccessKey AccessKey { get; }
    IRepository Repository { get; }
    IReadOnlyCollection<IRepositoryObject> Objects { get; }
}