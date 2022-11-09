using Backups.Models.Abstractions;

namespace Backups.Models.Storage.Abstractions;

public interface IStorage
{
    IRepositoryAccessKey AccessKey { get; }
    IRepository Repository { get; }
    IEnumerable<IRepositoryObject> Objects { get; }
}