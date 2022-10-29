using Backups.Models.Abstractions;

namespace Backups.Models.Algorithms;

public class SplitStorageAlgorithm : IStorageAlgorithm
{
    // TODO Get access key from invoker and make it abstract from saving in ZIP -> might be any means of saving
    public IEnumerable<ObjectStorageRelation> CreateStorage(IEnumerable<IBackupObject> backupObjects, IRepository targetRepository, IArchiver archiver)
    {
        var relations = new List<ObjectStorageRelation>();

        foreach (IBackupObject backupObject in backupObjects)
        {
            var ms = new MemoryStream();
            archiver.Archive(new List<IBackupObject> { backupObject }, ms);

            var storage = new Storage(
               targetRepository,
               new RepositoryAccessKey($"{backupObject.AccessKey.Value}.zip"),
               new List<RepositoryAccessKey> { backupObject.AccessKey },
               ms);
            relations.Add(new ObjectStorageRelation(backupObject, storage));
        }

        return relations.AsReadOnly();
    }
}