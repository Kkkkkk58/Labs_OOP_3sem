namespace Backups.Models.Abstractions;

public interface IRestorePointBuilder
{
    IRestorePointBuilder SetDate(DateTime restorePointDate);
    IRestorePointBuilder SetVersion(IRestorePointVersion restorePointVersion);
    IRestorePointBuilder SetRelations(IReadOnlyCollection<IObjectStorageRelation> objectStorageRelations);

    IRestorePoint Build();
}