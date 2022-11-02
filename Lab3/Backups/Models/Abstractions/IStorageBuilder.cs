namespace Backups.Models.Abstractions;

public interface IStorageBuilder
{
    IStorageBuilder SetRepository(IRepository repository);
    IStorageBuilder SetAccessKey(IRepositoryAccessKey repositoryAccessKey);
    IStorageBuilder SetBackupObjectAccessKeys(IReadOnlyList<IRepositoryAccessKey> backupObjectAccessKeys);

    IStorage Build();
}