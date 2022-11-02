using Backups.Models.Abstractions;

namespace Backups.Models;

public class StorageBuilder : IStorageBuilder
{
    private IRepository? _repository;
    private IRepositoryAccessKey? _repositoryAccessKey;
    private IReadOnlyList<IRepositoryAccessKey>? _backupObjectAccessKeys;

    public IStorageBuilder SetRepository(IRepository repository)
    {
        _repository = repository;
        return this;
    }

    public IStorageBuilder SetAccessKey(IRepositoryAccessKey repositoryAccessKey)
    {
        _repositoryAccessKey = repositoryAccessKey;
        return this;
    }

    public IStorageBuilder SetBackupObjectAccessKeys(IReadOnlyList<IRepositoryAccessKey> backupObjectAccessKeys)
    {
        _backupObjectAccessKeys = backupObjectAccessKeys;
        return this;
    }

    public IStorage Build()
    {
        ArgumentNullException.ThrowIfNull(_repository);
        ArgumentNullException.ThrowIfNull(_repositoryAccessKey);
        ArgumentNullException.ThrowIfNull(_backupObjectAccessKeys);

        var result = new Storage(_repository, _repositoryAccessKey, _backupObjectAccessKeys);
        Reset();

        return result;
    }

    private void Reset()
    {
        _repository = null;
        _repositoryAccessKey = null;
        _backupObjectAccessKeys = null;
    }
}