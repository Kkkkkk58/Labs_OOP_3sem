using Backups.Models.Abstractions;

namespace Backups.Models.Algorithms;

public class SplitStorageAlgorithm : IStorageAlgorithm
{
    public IEnumerable<ObjectStorageRelation> CreateStorage(
        IEnumerable<IBackupObject> backupObjects,
        IRepository targetRepository,
        IArchiver archiver,
        IRepositoryAccessKey baseAccessKey)
    {
        var relations = new List<ObjectStorageRelation>();

        foreach (IBackupObject backupObject in backupObjects)
        {
            IRepositoryAccessKey key = baseAccessKey.CombineWithSeparator(backupObject.AccessKey)
                .CombineWithExtension(archiver.Extension);
            using Stream stream = targetRepository.OpenStream(key);
            archiver.Archive(new List<IBackupObject> { backupObject }, stream);

            var storage = new Storage(
                targetRepository,
                key,
                new List<IRepositoryAccessKey> { backupObject.AccessKey },
                stream);
            relations.Add(new ObjectStorageRelation(backupObject, storage));

            stream.Dispose();
        }

        return relations.AsReadOnly();
    }
}