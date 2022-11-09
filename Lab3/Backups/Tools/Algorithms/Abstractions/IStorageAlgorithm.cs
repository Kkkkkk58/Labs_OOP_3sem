using Backups.Models.Abstractions;
using Backups.Models.Storage.Abstractions;
using Backups.Tools.Archiver.Abstractions;

namespace Backups.Tools.Algorithms.Abstractions;

public interface IStorageAlgorithm
{
    IStorage CreateStorage(
        IReadOnlyCollection<IBackupObject> backupObjects,
        IRepository targetRepository,
        IArchiver archiver,
        IRepositoryAccessKey baseAccessKey);
}