using Backups.Models.Abstractions;
using Backups.Models.Repository.Abstractions;
using Backups.Models.RepositoryObjects.Abstractions;
using Backups.Models.Storage.Abstractions;

namespace Backups.Models.Storage;

public class SplitStorage : IStorage
{
    private readonly IReadOnlyCollection<IStorage> _innerStorage;

    public SplitStorage(
        IRepository repository,
        IRepositoryAccessKey accessKey,
        IReadOnlyCollection<IStorage> innerStorage)
    {
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(accessKey);
        ArgumentNullException.ThrowIfNull(innerStorage);

        _innerStorage = innerStorage;
        AccessKey = accessKey;
        Repository = repository;
    }

    public IRepositoryAccessKey AccessKey { get; }
    public IRepository Repository { get; }
    public IReadOnlyCollection<IRepositoryObject> Objects => _innerStorage.SelectMany(storage => storage.Objects).ToList();

    public override string ToString()
    {
        return $"SplitStorage {AccessKey} saved into {Repository}";
    }
}