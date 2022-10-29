using Backups.Models.Abstractions;

namespace Backups.Models.Algorithms;

public class SingleStorageAlgorithm : IStorageAlgorithm
{
    public IEnumerable<ObjectStorageRelation> CreateStorage(IEnumerable<IBackupObject> backupObjects, IRepository targetRepository, IArchiver archiver, IRepositoryAccessKey baseAccessKey)
    {
        IRepositoryAccessKey key = baseAccessKey.CombineWithSeparator(Guid.NewGuid().ToString()).CombineWithExtension(archiver.Extension);
        using Stream writingStream = targetRepository.OpenStream(key);
        var copy = backupObjects.ToList();
        var keys = copy.Select(bo => bo.AccessKey).ToList();
        archiver.Archive(copy, writingStream);

        var storage = new Storage(
            targetRepository,
            key,
            keys,
            writingStream);

        writingStream.Dispose();
        return copy.Select(backupObject => new ObjectStorageRelation(backupObject, storage)).ToList().AsReadOnly();
    }
}