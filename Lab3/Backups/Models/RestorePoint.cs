namespace Backups.Models;

public class RestorePoint
{
    public RestorePoint(DateTime creationDate, int versionNumber, IReadOnlyCollection<ObjectStorageRelation> objectStorageRelations)
    {
        ArgumentNullException.ThrowIfNull(objectStorageRelations);
        if (versionNumber <= 0)
            throw new ArgumentOutOfRangeException(nameof(versionNumber));
        if (ContainsRepeatingBackupObjects(objectStorageRelations))
            throw new NotImplementedException();

        CreationDate = creationDate;
        VersionNumber = versionNumber;
        ObjectStorageRelations = objectStorageRelations.ToList();
    }

    // TODO GLOBAL add init to props
    public DateTime CreationDate { get; }
    public int VersionNumber { get; }
    public IReadOnlyList<ObjectStorageRelation> ObjectStorageRelations { get; }

    private static bool ContainsRepeatingBackupObjects(IReadOnlyCollection<ObjectStorageRelation> objectStorageRelations)
    {
        return objectStorageRelations.DistinctBy(o => o.BackupObject).Count() != objectStorageRelations.Count;
    }
}