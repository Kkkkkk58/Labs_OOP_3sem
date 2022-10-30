namespace Backups.Models.Abstractions;

public interface IRestorePoint
{
    DateTime CreationDate { get; }
    IRestorePointVersion Version { get; }
    IReadOnlyList<IObjectStorageRelation> ObjectStorageRelations { get; }
}