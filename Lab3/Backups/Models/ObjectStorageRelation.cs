using Backups.Models.Abstractions;

namespace Backups.Models;

public record ObjectStorageRelation : IObjectStorageRelation
{
    public ObjectStorageRelation(IBackupObject backupObject, IStorage storage)
    {
        ArgumentNullException.ThrowIfNull(backupObject);
        ArgumentNullException.ThrowIfNull(storage);
        if (!storage.BackupObjectKeys.Contains(backupObject.AccessKey))
            throw new NotImplementedException();

        BackupObject = backupObject;
        Storage = storage;
    }

    public IBackupObject BackupObject { get; }
    public IStorage Storage { get; }
}