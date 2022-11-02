using Backups.Models.Abstractions;

namespace Backups.Models;

public class Storage : IStorage
{
    public Storage(
        IRepository repository,
        IRepositoryAccessKey accessKey,
        IReadOnlyList<IRepositoryAccessKey> backupObjectKeys)
    {
        ArgumentNullException.ThrowIfNull(accessKey);
        ArgumentNullException.ThrowIfNull(backupObjectKeys);
        ArgumentNullException.ThrowIfNull(repository);

        AccessKey = accessKey;
        Repository = repository;
        BackupObjectKeys = backupObjectKeys;
    }

    public IRepositoryAccessKey AccessKey { get; }
    public IRepository Repository { get; }
    public IReadOnlyList<IRepositoryAccessKey> BackupObjectKeys { get; }

    public override string ToString()
    {
        return $"Storage: {Repository} - {AccessKey}";
    }
}