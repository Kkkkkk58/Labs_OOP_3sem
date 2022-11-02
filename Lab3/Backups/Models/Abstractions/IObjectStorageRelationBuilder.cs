namespace Backups.Models.Abstractions;

public interface IObjectStorageRelationBuilder
{
    IRelationStorageBuilder SetBackupObject(IBackupObject backupObject);

    IObjectStorageRelation Build();
}

public interface IRelationStorageBuilder
{
    IObjectStorageRelationBuilder SetStorage(IStorage storage);
}