using Backups.Models.Abstractions;

namespace Backups.Models;

public class Storage : IStorage
{
    private Storage(
        IRepository repository,
        IRepositoryAccessKey accessKey,
        IReadOnlyList<IRepositoryAccessKey> backupObjectKeys)
    {
        AccessKey = accessKey;
        Repository = repository;
        BackupObjectKeys = backupObjectKeys;
    }

    public static IStorageBuilder Builder => new StorageBuilder();

    public IRepositoryAccessKey AccessKey { get; }
    public IRepository Repository { get; }
    public IReadOnlyList<IRepositoryAccessKey> BackupObjectKeys { get; }

    public override string ToString()
    {
        return $"Storage: {Repository} - {AccessKey}";
    }

    public class StorageBuilder : IStorageBuilder, IStorageAccessKeyBuilder, IStorageBackupObjectAccessKeysBuilder
    {
        private IRepository? _repository;
        private IRepositoryAccessKey? _repositoryAccessKey;
        private IReadOnlyList<IRepositoryAccessKey>? _backupObjectAccessKeys;

        public IStorageAccessKeyBuilder SetRepository(IRepository repository)
        {
            _repository = repository;
            return this;
        }

        public IStorageBackupObjectAccessKeysBuilder SetAccessKey(IRepositoryAccessKey repositoryAccessKey)
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
}