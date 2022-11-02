using Backups.Models.Abstractions;
using Backups.Tools.Archivers.Abstractions;

namespace Backups.Tools.Algorithms.Abstractions;

public interface IStorageAlgorithm
{
    IEnumerable<IObjectStorageRelation> CreateStorage(
        IReadOnlyCollection<IBackupObject> backupObjects,
        IRepository targetRepository,
        IArchiver archiver,
        IRepositoryAccessKey baseAccessKey);
}