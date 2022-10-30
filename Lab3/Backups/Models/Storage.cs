using Backups.Models.Abstractions;

namespace Backups.Models;

public class Storage
{
    public Storage(
        IRepository repository,
        IRepositoryAccessKey accessKey,
        IReadOnlyList<IRepositoryAccessKey> backupObjectKeys,
        Stream backupObjectData)
    {
        ArgumentNullException.ThrowIfNull(accessKey);
        ArgumentNullException.ThrowIfNull(backupObjectKeys);
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(backupObjectData);

        AccessKey = accessKey;
        Repository = repository;
        BackupObjectKeys = backupObjectKeys;
    }

    public IRepositoryAccessKey AccessKey { get; }
    public IRepository Repository { get; }
    public IReadOnlyList<IRepositoryAccessKey> BackupObjectKeys { get; }
}