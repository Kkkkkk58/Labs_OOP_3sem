using Backups.Models.Abstractions;

namespace Backups.Models.Algorithms;

public class SingleStorageAlgorithm : IStorageAlgorithm
{
    public IEnumerable<IObjectStorageRelation> CreateStorage(
        IReadOnlyCollection<IBackupObject> backupObjects,
        IRepository targetRepository,
        IArchiver archiver,
        IRepositoryAccessKey baseAccessKey)
    {
        ValidateArguments(backupObjects, targetRepository, archiver, baseAccessKey);

        IRepositoryAccessKey storageKey = GetStorageKey(baseAccessKey, archiver.Extension);

        using Stream writingStream = targetRepository.OpenStream(storageKey);
        archiver.Archive(backupObjects, writingStream);

        var backupObjectKeys = backupObjects.Select(bo => bo.AccessKey).ToList();
        var storage = new Storage(targetRepository, storageKey, backupObjectKeys);

        writingStream.Dispose();
        return backupObjects
            .Select(backupObject => new ObjectStorageRelation(backupObject, storage));
    }

    private static IRepositoryAccessKey GetStorageKey(IRepositoryAccessKey baseAccessKey, string archiverExtension)
    {
        string storageName = Guid.NewGuid().ToString();

        return baseAccessKey
            .Combine(storageName)
            .ApplyExtension(archiverExtension);
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