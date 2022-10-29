using Backups.Models.Abstractions;

namespace Backups.Models;

public class Storage
{
    public Storage(IRepository repository, RepositoryAccessKey accessKey, IReadOnlyList<RepositoryAccessKey> backupObjectKeys, Stream backupObjectData)
    {
        ArgumentNullException.ThrowIfNull(accessKey);
        ArgumentNullException.ThrowIfNull(backupObjectKeys);
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(backupObjectData);

        AccessKey = accessKey;
        Repository = repository;
        BackupObjectKeys = backupObjectKeys;

        Repository.SaveData(AccessKey, backupObjectData);
    }

    public RepositoryAccessKey AccessKey { get; }
    public IRepository Repository { get; }
    public IReadOnlyList<RepositoryAccessKey> BackupObjectKeys { get; }
}