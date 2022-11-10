using Backups.Models.Abstractions;
using Backups.Models.Repository.Abstractions;
using Backups.Models.RepositoryObjects.Abstractions;
using Backups.Models.Storage.Abstractions;
using Backups.Tools.Algorithms.Abstractions;
using Backups.Tools.Archiver.Abstractions;

namespace Backups.Tools.Algorithms;

public class SingleStorageAlgorithm : IStorageAlgorithm
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

        IRepositoryAccessKey storageKey = GetStorageKey(baseAccessKey);
        IEnumerable<IRepositoryObject> repositoryObjects = backupObjects
            .Select(bo => bo.GetRepositoryObject());

        return archiver.Archive(repositoryObjects, targetRepository, storageKey);
    }

    private static IRepositoryAccessKey GetStorageKey(IRepositoryAccessKey baseAccessKey)
    {
        string storageName = Guid.NewGuid().ToString();

        return baseAccessKey.Combine(storageName);
    }
}