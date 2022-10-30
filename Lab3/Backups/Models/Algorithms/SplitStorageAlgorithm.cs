using Backups.Models.Abstractions;

namespace Backups.Models.Algorithms;

public class SplitStorageAlgorithm : IStorageAlgorithm
{
    public IEnumerable<IObjectStorageRelation> CreateStorage(
        IReadOnlyCollection<IBackupObject> backupObjects,
        IRepository targetRepository,
        IArchiver archiver,
        IRepositoryAccessKey baseAccessKey)
    {
        ValidateArguments(backupObjects, targetRepository, archiver, baseAccessKey);

        var relations = new List<IObjectStorageRelation>();

        foreach (IBackupObject backupObject in backupObjects)
        {
            IRepositoryAccessKey storageKey = GetStorageKey(baseAccessKey, backupObject.AccessKey, archiver.Extension);
            using Stream stream = targetRepository.OpenStream(storageKey);
            archiver.Archive(new List<IBackupObject> { backupObject }, stream);
            var backupObjectKeys = new List<IRepositoryAccessKey> { backupObject.AccessKey };

            var storage = new Storage(targetRepository, storageKey, backupObjectKeys);
            relations.Add(new ObjectStorageRelation(backupObject, storage));

            stream.Dispose();
        }

        return relations.AsReadOnly();
    }

    private static IRepositoryAccessKey GetStorageKey(
        IRepositoryAccessKey baseAccessKey,
        IRepositoryAccessKey backupObjectKey,
        string extension)
    {
        return baseAccessKey
            .Combine(backupObjectKey)
            .ApplyExtension(extension);
    }

    private static void ValidateArguments(
        IReadOnlyCollection<IBackupObject> backupObjects,
        IRepository targetRepository,
        IArchiver archiver,
        IRepositoryAccessKey baseAccessKey)
    {
        ArgumentNullException.ThrowIfNull(backupObjects);
        ArgumentNullException.ThrowIfNull(targetRepository);
        ArgumentNullException.ThrowIfNull(archiver);
        ArgumentNullException.ThrowIfNull(baseAccessKey);
    }
}