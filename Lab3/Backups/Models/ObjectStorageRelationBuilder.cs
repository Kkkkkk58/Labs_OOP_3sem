using Backups.Models.Abstractions;

namespace Backups.Models;

public class ObjectStorageRelationBuilder : IObjectStorageRelationBuilder
{
    private IBackupObject? _backupObject;
    private IStorage? _storage;

    public IObjectStorageRelationBuilder SetBackupObject(IBackupObject backupObject)
    {
        _backupObject = backupObject;
        return this;
    }

    public IObjectStorageRelationBuilder SetStorage(IStorage storage)
    {
        _storage = storage;
        return this;
    }

    public IObjectStorageRelation Build()
    {
        ArgumentNullException.ThrowIfNull(_backupObject);
        ArgumentNullException.ThrowIfNull(_storage);

        var relation = new ObjectStorageRelation(_backupObject, _storage);
        Reset();
        return relation;
    }

    private void Reset()
    {
        _backupObject = null;
        _storage = null;
    }
}