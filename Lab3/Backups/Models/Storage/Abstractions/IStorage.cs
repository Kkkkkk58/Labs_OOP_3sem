using Backups.Models.Abstractions;
using Backups.Models.Repository.Abstractions;

namespace Backups.Models.Storage.Abstractions;

public interface IStorage
{
    IRepositoryAccessKey AccessKey { get; }
    IRepository Repository { get; }
    IEnumerable<IRepositoryObject> Objects { get; }
}