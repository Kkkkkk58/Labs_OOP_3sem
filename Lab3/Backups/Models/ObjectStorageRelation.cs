using Backups.Models.Abstractions;

namespace Backups.Models;

public record ObjectStorageRelation
{
    public ObjectStorageRelation(Abstractions.IBackupObject backupObject, Storage storage)
    {
        ArgumentNullException.ThrowIfNull(backupObject);
        ArgumentNullException.ThrowIfNull(storage);
        if (!storage.BackupObjectKeys.Contains(backupObject.AccessKey))
            throw new NotImplementedException();

        BackupObject = backupObject;
        Storage = storage;
    }

    public Abstractions.IBackupObject BackupObject { get; }
    public Storage Storage { get; }
}