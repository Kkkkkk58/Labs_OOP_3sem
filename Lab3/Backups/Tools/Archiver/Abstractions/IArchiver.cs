using Backups.Models.Abstractions;
using Backups.Models.Repository.Abstractions;
using Backups.Models.Storage.Abstractions;

namespace Backups.Tools.Archiver.Abstractions;

public interface IArchiver
{
    IStorage Archive(IEnumerable<IRepositoryObject> repositoryObjects, IRepository repository, IRepositoryAccessKey partialArchiveKey);
}