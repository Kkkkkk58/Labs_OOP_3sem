using Backups.Models.Abstractions;
using Backups.Tools.Algorithms.Abstractions;
using Backups.Tools.Archivers.Abstractions;

namespace Backups.Tools.Algorithms;

public class SingleStorageAlgorithm : IStorageAlgorithm
{
    private readonly IStorageBuilder _storageBuilder;
    private readonly IObjectStorageRelationBuilder _objectStorageRelationBuilder;

    public SingleStorageAlgorithm(
        IStorageBuilder storageBuilder,
        IObjectStorageRelationBuilder objectStorageRelationBuilder)
    {
        ArgumentNullException.ThrowIfNull(storageBuilder);
        ArgumentNullException.ThrowIfNull(objectStorageRelationBuilder);

        _storageBuilder = storageBuilder;
        _objectStorageRelationBuilder = objectStorageRelationBuilder;
    }

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
        IStorage storage = GetStorage(targetRepository, storageKey, backupObjectKeys);

        return backupObjects
            .Select(backupObject => GetObjectStorageRelation(backupObject, storage));
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

    private IStorage GetStorage(
        IRepository targetRepository,
        IRepositoryAccessKey storageKey,
        IReadOnlyList<IRepositoryAccessKey> backupObjectKeys)
    {
        return _storageBuilder
            .SetRepository(targetRepository)
            .SetAccessKey(storageKey)
            .SetBackupObjectAccessKeys(backupObjectKeys)
            .Build();
    }

    private IObjectStorageRelation GetObjectStorageRelation(IBackupObject backupObject, IStorage storage)
    {
        return _objectStorageRelationBuilder
            .SetBackupObject(backupObject)
            .SetStorage(storage)
            .Build();
    }
}