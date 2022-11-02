namespace Backups.Models.Abstractions;

public interface IRestorePointBuilder
{
    IRestorePointVersionBuilder SetDate(DateTime restorePointDate);
    IRestorePoint Build();
}

public interface IRestorePointVersionBuilder
{
    IRestorePointRelationsBuilder SetVersion(IRestorePointVersion restorePointVersion);
}

public interface IRestorePointRelationsBuilder
{
    IRestorePointBuilder SetRelations(IReadOnlyList<IObjectStorageRelation> objectStorageRelations);
}