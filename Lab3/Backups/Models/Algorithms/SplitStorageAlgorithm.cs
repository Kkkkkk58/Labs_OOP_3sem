using Backups.Models.Abstractions;

namespace Backups.Models.Algorithms;

public class SplitStorageAlgorithm : IStorageAlgorithm
{
    private readonly IStorageBuilder _storageBuilder;
    private readonly IObjectStorageRelationBuilder _objectStorageRelationBuilder;

    public SplitStorageAlgorithm(IStorageBuilder storageBuilder, IObjectStorageRelationBuilder objectStorageRelationBuilder)
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

        var relations = new List<IObjectStorageRelation>();

        foreach (IBackupObject backupObject in backupObjects)
        {
            IRepositoryAccessKey storageKey = GetStorageKey(baseAccessKey, backupObject.AccessKey, archiver.Extension);
            using Stream stream = targetRepository.OpenStream(storageKey);
            archiver.Archive(new List<IBackupObject> { backupObject }, stream);
            var backupObjectKeys = new List<IRepositoryAccessKey> { backupObject.AccessKey };

            IStorage storage = GetStorage(targetRepository, storageKey, backupObjectKeys);
            IObjectStorageRelation relation = GetObjectStorageRelation(backupObject, storage);
            relations.Add(relation);

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

    private IStorage GetStorage(IRepository targetRepository, IRepositoryAccessKey storageKey, IReadOnlyList<IRepositoryAccessKey> backupObjectKeys)
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