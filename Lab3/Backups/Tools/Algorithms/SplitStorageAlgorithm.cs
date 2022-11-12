using Backups.Models.Abstractions;
using Backups.Models.Repository.Abstractions;
using Backups.Models.RepositoryObjects.Abstractions;
using Backups.Models.Storage;
using Backups.Models.Storage.Abstractions;
using Backups.Tools.Algorithms.Abstractions;
using Backups.Tools.Archiver.Abstractions;

namespace Backups.Tools.Algorithms;

public class SplitStorageAlgorithm : IStorageAlgorithm
{
    public IStorage CreateStorage(
        IReadOnlyCollection<IBackupObject> backupObjects,
        IRepository targetRepository,
        IArchiver archiver,
        IRepositoryAccessKey baseAccessKey)
    {
        ArgumentNullException.ThrowIfNull(backupObjects);
        ArgumentNullException.ThrowIfNull(targetRepository);
        ArgumentNullException.ThrowIfNull(archiver);
        ArgumentNullException.ThrowIfNull(baseAccessKey);

        var innerStorage = backupObjects
            .Select(backupObject => GetBackupObjectStorage(targetRepository, archiver, baseAccessKey, backupObject))
            .ToList();

        return new SplitStorage(targetRepository, baseAccessKey, innerStorage);
    }

    private static IStorage GetBackupObjectStorage(
        IRepository targetRepository,
        IArchiver archiver,
        IRepositoryAccessKey baseAccessKey,
        IBackupObject backupObject)
    {
        IRepositoryObject[] repositoryObjects = { backupObject.GetRepositoryObject() };
        IRepositoryAccessKey storageKey = GetStorageKey(baseAccessKey, backupObject.AccessKey.Name);

        return archiver.Archive(repositoryObjects, targetRepository, storageKey);
    }

    private static IRepositoryAccessKey GetStorageKey(IRepositoryAccessKey baseAccessKey, string backupObjectName)
    {
        return baseAccessKey.Combine(backupObjectName);
    }
}