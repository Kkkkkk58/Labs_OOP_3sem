using Backups.Archivers.Abstractions;
using Backups.Models.Abstractions;

namespace Backups.Algorithms.Abstractions;

public interface IStorageAlgorithm
{
    IEnumerable<IObjectStorageRelation> CreateStorage(
        IReadOnlyCollection<IBackupObject> backupObjects,
        IRepository targetRepository,
        IArchiver archiver,
        IRepositoryAccessKey baseAccessKey);
}