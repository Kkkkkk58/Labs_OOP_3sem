using Backups.Exceptions;
using Backups.Models.Abstractions;

namespace Backups.Models;

public record ObjectStorageRelation : IObjectStorageRelation
{
    private ObjectStorageRelation(IBackupObject backupObject, IStorage storage)
    {
        BackupObject = backupObject;
        Storage = storage;
    }

    public IBackupObject BackupObject { get; }
    public IStorage Storage { get; }
    public static IObjectStorageRelationBuilder Builder => new ObjectStorageRelationBuilder();

    public class ObjectStorageRelationBuilder : IObjectStorageRelationBuilder, IRelationStorageBuilder
    {
        private IBackupObject? _backupObject;
        private IStorage? _storage;

        public IRelationStorageBuilder SetBackupObject(IBackupObject backupObject)
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
            if (!_storage.BackupObjectKeys.Contains(_backupObject.AccessKey))
                throw ObjectStorageRelationException.InvalidRelation();

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
}