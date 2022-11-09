using Backups.Models.Abstractions;
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
        ValidateArguments(backupObjects, targetRepository, archiver, baseAccessKey);

        var innerStorage = backupObjects
            .Select(backupObject => archiver.Archive(new List<IRepositoryObject> { backupObject.GetRepositoryObject() }, targetRepository, GetStorageKey(baseAccessKey,  backupObject.AccessKey)))
            .ToList();

        return new SplitStorage(targetRepository, baseAccessKey, innerStorage);
    }

    private static IRepositoryAccessKey GetStorageKey(IRepositoryAccessKey baseAccessKey, IRepositoryAccessKey backupObjectKey)
    {
        return baseAccessKey
            .Combine(backupObjectKey);
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