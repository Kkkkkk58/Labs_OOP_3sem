using Backups.Models.Abstractions;

namespace Backups.Models.Algorithms;

public class SingleStorageAlgorithm : IStorageAlgorithm
{
    public IEnumerable<ObjectStorageRelation> CreateStorage(IEnumerable<Abstractions.IBackupObject> backupObjects, IRepository targetRepository, IArchiver archiver)
    {
        using var ms = new MemoryStream();
        var copy = backupObjects.ToList();
        var keys = copy.Select(bo => bo.AccessKey).ToList();
        archiver.Archive(copy, ms);

        var storage = new Storage(
            targetRepository,
            new RepositoryAccessKey("test.zip"),
            keys,
            ms);
        return copy.Select(backupObject => new ObjectStorageRelation(backupObject, storage)).ToList().AsReadOnly();
    }
}