using Backups.Models.Abstractions;

namespace Backups.Models;

public class RestorePoint : IRestorePoint
{
    public RestorePoint(
        DateTime creationDate,
        IRestorePointVersion version,
        IReadOnlyCollection<IObjectStorageRelation> objectStorageRelations)
    {
        ArgumentNullException.ThrowIfNull(objectStorageRelations);
        if (ContainsRepeatingBackupObjects(objectStorageRelations))
            throw new NotImplementedException();

        CreationDate = creationDate;
        Version = version;
        ObjectStorageRelations = objectStorageRelations.ToList();
    }

    public DateTime CreationDate { get; }
    public IRestorePointVersion Version { get; }
    public IReadOnlyList<IObjectStorageRelation> ObjectStorageRelations { get; }

    private static bool ContainsRepeatingBackupObjects(
        IReadOnlyCollection<IObjectStorageRelation> objectStorageRelations)
    {
        return objectStorageRelations.DistinctBy(o => o.BackupObject).Count() != objectStorageRelations.Count;
    }
}