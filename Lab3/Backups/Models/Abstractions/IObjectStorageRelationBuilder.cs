namespace Backups.Models.Abstractions;

public interface IObjectStorageRelationBuilder
{
    IObjectStorageRelationBuilder SetBackupObject(IBackupObject backupObject);
    IObjectStorageRelationBuilder SetStorage(IStorage storage);

    IObjectStorageRelation Build();
}