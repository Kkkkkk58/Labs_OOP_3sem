using Backups.Exceptions;
using Backups.Models.Abstractions;

namespace Backups.Models;

public class RestorePoint : IRestorePoint
{
    private RestorePoint(
        DateTime creationDate,
        IRestorePointVersion version,
        IReadOnlyList<IObjectStorageRelation> objectStorageRelations)
    {
        CreationDate = creationDate;
        Version = version;
        ObjectStorageRelations = objectStorageRelations;
    }

    public static IRestorePointBuilder Builder => new RestorePointBuilder();
    public DateTime CreationDate { get; }
    public IRestorePointVersion Version { get; }
    public IReadOnlyList<IObjectStorageRelation> ObjectStorageRelations { get; }

    public override string ToString()
    {
        return $"Restore point {Version} from {CreationDate}";
    }

    public class RestorePointBuilder : IRestorePointBuilder, IRestorePointVersionBuilder, IRestorePointRelationsBuilder
    {
        private DateTime? _restorePointDate;
        private IRestorePointVersion? _restorePointVersion;
        private IReadOnlyList<IObjectStorageRelation>? _objectStorageRelations;

        public IRestorePointVersionBuilder SetDate(DateTime restorePointDate)
        {
            _restorePointDate = restorePointDate;
            return this;
        }

        public IRestorePointRelationsBuilder SetVersion(IRestorePointVersion restorePointVersion)
        {
            _restorePointVersion = restorePointVersion;
            return this;
        }

        public IRestorePointBuilder SetRelations(IReadOnlyList<IObjectStorageRelation> objectStorageRelations)
        {
            if (objectStorageRelations is not null && ContainsRepeatingBackupObjects(objectStorageRelations))
                throw RestorePointException.ContainsRepeatingBackupObjects();

            _objectStorageRelations = objectStorageRelations;
            return this;
        }

        public IRestorePoint Build()
        {
            ArgumentNullException.ThrowIfNull(_restorePointDate);
            ArgumentNullException.ThrowIfNull(_restorePointVersion);
            ArgumentNullException.ThrowIfNull(_objectStorageRelations);

            var restorePoint = new RestorePoint(_restorePointDate.Value, _restorePointVersion, _objectStorageRelations);
            Reset();

            return restorePoint;
        }

        private static bool ContainsRepeatingBackupObjects(
            IReadOnlyCollection<IObjectStorageRelation> objectStorageRelations)
        {
            return objectStorageRelations.DistinctBy(o => o.BackupObject).Count() != objectStorageRelations.Count;
        }

        private void Reset()
        {
            _objectStorageRelations = null;
            _restorePointDate = null;
            _restorePointVersion = null;
        }
    }
}