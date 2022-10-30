using Backups.Models.Abstractions;

namespace Backups.Models.Algorithms;

public interface IStorageAlgorithm
{
    IEnumerable<ObjectStorageRelation> CreateStorage(
        IEnumerable<IBackupObject> backupObjects,
        IRepository targetRepository,
        IArchiver archiver,
        IRepositoryAccessKey baseAccessKey);
}