using Backups.Models.Abstractions;

namespace Backups.Models.Algorithms;

public interface IStorageAlgorithm
{
    IEnumerable<IObjectStorageRelation> CreateStorage(
        IReadOnlyCollection<IBackupObject> backupObjects,
        IRepository targetRepository,
        IArchiver archiver,
        IRepositoryAccessKey baseAccessKey);
}