namespace Backups.Models.Abstractions;

public interface IStorageBuilder
{
    IStorageAccessKeyBuilder SetRepository(IRepository repository);
    IStorage Build();
}

public interface IStorageAccessKeyBuilder
{
    IStorageBackupObjectAccessKeysBuilder SetAccessKey(IRepositoryAccessKey repositoryAccessKey);
}

public interface IStorageBackupObjectAccessKeysBuilder
{
    IStorageBuilder SetBackupObjectAccessKeys(IReadOnlyList<IRepositoryAccessKey> backupObjectAccessKeys);
}