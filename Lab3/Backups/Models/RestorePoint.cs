using Backups.Models.Abstractions;
using Backups.Models.Storage.Abstractions;

namespace Backups.Models;

public class RestorePoint : IRestorePoint
{
    private RestorePoint(
        Guid id,
        DateTime creationDate,
        IEnumerable<IBackupObject> backupObjects,
        IStorage storage)
    {
        ArgumentNullException.ThrowIfNull(backupObjects);
        ArgumentNullException.ThrowIfNull(storage);

        Id = id;
        CreationDate = creationDate;
        BackupObjects = backupObjects;
        Storage = storage;
    }

    public static IRestorePointBuilder Builder => new RestorePointBuilder();
    public Guid Id { get; }
    public DateTime CreationDate { get; }
    public IStorage Storage { get; }
    public IEnumerable<IBackupObject> BackupObjects { get; }

    public override string ToString()
    {
        return $"Restore point {Id} from {CreationDate}";
    }

    public class RestorePointBuilder : IRestorePointBuilder, IRestorePointBackupObjectsBuilder, IRestorePointStorageBuilder
    {
        private Guid? _id;
        private DateTime? _restorePointDate;
        private IEnumerable<IBackupObject>? _backupObjects;
        private IStorage? _storage;

        public IRestorePointBuilder SetId(Guid id)
        {
            _id = id;
            return this;
        }

        public IRestorePointBackupObjectsBuilder SetDate(DateTime restorePointDate)
        {
            _restorePointDate = restorePointDate;
            return this;
        }

        public IRestorePointStorageBuilder SetBackupObjects(IEnumerable<IBackupObject> backupObjects)
        {
            _backupObjects = backupObjects;
            return this;
        }

        public IRestorePointBuilder SetStorage(IStorage storage)
        {
            _storage = storage;
            return this;
        }

        public IRestorePoint Build()
        {
            _id ??= Guid.NewGuid();
            ArgumentNullException.ThrowIfNull(_restorePointDate);
            ArgumentNullException.ThrowIfNull(_backupObjects);
            ArgumentNullException.ThrowIfNull(_storage);

            var restorePoint = new RestorePoint(_id.Value, _restorePointDate.Value, _backupObjects, _storage);

            Reset();
            return restorePoint;
        }

        private void Reset()
        {
            _id = null;
            _restorePointDate = null;
            _backupObjects = null;
            _storage = null;
        }
    }
}