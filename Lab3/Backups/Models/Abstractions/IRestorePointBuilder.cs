using Backups.Models.Storage.Abstractions;

namespace Backups.Models.Abstractions;

public interface IRestorePointBuilder
{
    IRestorePointBuilder SetId(Guid id);
    IRestorePointBackupObjectsBuilder SetDate(DateTime restorePointDate);

    IRestorePoint Build();
}

public interface IRestorePointBackupObjectsBuilder
{
    IRestorePointStorageBuilder SetBackupObjects(IEnumerable<IBackupObject> backupObjects);
}

public interface IRestorePointStorageBuilder
{
    IRestorePointBuilder SetStorage(IStorage storage);
}