using Backups.Models.Abstractions;
using Backups.Models.Storage.Abstractions;

namespace Backups.Models.Storage;

public class ZipStorage : IStorage
{
    private readonly IEnumerable<IArchivedObject> _archivedObjects;

    public ZipStorage(IRepositoryAccessKey accessKey, IRepository repository, IEnumerable<IArchivedObject> objects)
    {
        AccessKey = accessKey;
        Repository = repository;
        _archivedObjects = objects;
    }

    public IRepositoryAccessKey AccessKey { get; }
    public IRepository Repository { get; }
    public IEnumerable<IRepositoryObject> Objects => _archivedObjects.Select(ao => ao.GetRepositoryObject(null!));
}